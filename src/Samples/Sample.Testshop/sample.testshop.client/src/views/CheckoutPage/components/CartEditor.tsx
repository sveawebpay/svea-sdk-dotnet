import React, { useEffect } from "react";
import { useForm, useFieldArray } from "react-hook-form";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  TextField,
  IconButton,
  Button,
} from "@mui/material";
import { OrderRowResponse } from "../../../shared/models/checkout/shared";
import { AddOutlined, Delete } from "@mui/icons-material";

interface CartEditorProps {
  initialItems: OrderRowResponse[];
}

const CartEditor: React.FC<CartEditorProps> = ({ initialItems }) => {
  const { control, register, handleSubmit, setValue } = useForm({
    defaultValues: {
      items: initialItems || [],
    },
  });
  const { fields, append, remove } = useFieldArray({
    control,
    name: "items",
  });

  useEffect(() => {
    if (initialItems && initialItems.length > 0) {
      console.log(initialItems);
      initialItems.forEach((item, index) => {
        setValue(`items.${index}`, item);
      });
    }
  }, [initialItems, setValue]);

  const onSubmit = (data: { items: OrderRowResponse[] }) => {
    console.log(data);
  };

  const handleAddRow = () => {
    append({
      articleNumber: "",
      name: "",
      quantity: { inLowestMonetaryUnit: 100 },
      unitPrice: { inLowestMonetaryUnit: 0 },
      unit: "",
      vatPercent: { inLowestMonetaryUnit: 0 },
      rowNumber: fields.length + 1,
    });
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Article Number</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Quantity</TableCell>
              <TableCell>Unit Price</TableCell>
              <TableCell>Unit</TableCell>
              <TableCell>Discount Amount</TableCell>
              <TableCell>Discount Percent</TableCell>
              <TableCell>Discount Value</TableCell>
              <TableCell>Merchant Data</TableCell>
              <TableCell>Shipping Info</TableCell>
              <TableCell>Temporary Reference</TableCell>
              <TableCell>VAT Percent</TableCell>
              <TableCell>Row Number</TableCell>
              <TableCell>Row Type</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {fields.map((item, index) => (
              <TableRow key={item.id}>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.articleNumber`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.name`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(
                      `items.${index}.quantity.inLowestMonetaryUnit`,
                      {
                        valueAsNumber: true,
                      }
                    )}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(
                      `items.${index}.unitPrice.inLowestMonetaryUnit`,
                      {
                        valueAsNumber: true,
                      }
                    )}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.unit`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(
                      `items.${index}.discountAmount.inLowestMonetaryUnit`,
                      {
                        valueAsNumber: true,
                      }
                    )}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(
                      `items.${index}.discountPercent.inLowestMonetaryUnit`,
                      {
                        valueAsNumber: true,
                      }
                    )}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(
                      `items.${index}.discountValue.inLowestMonetaryUnit`,
                      {
                        valueAsNumber: true,
                      }
                    )}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.merchantData`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.shippingInfo`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.temporaryReference`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(
                      `items.${index}.vatPercent.inLowestMonetaryUnit`,
                      {
                        valueAsNumber: true,
                      }
                    )}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.rowNumber`, {
                      valueAsNumber: true,
                    })}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <TextField
                    size="small"
                    {...register(`items.${index}.rowType`)}
                    variant="outlined"
                  />
                </TableCell>
                <TableCell>
                  <IconButton onClick={() => remove(index)}>
                    <Delete />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Button
        startIcon={<AddOutlined />}
        onClick={handleAddRow}
        variant="contained"
        color="primary"
        sx={{ mt: 2 }}
      >
        Add Row
      </Button>
      <Button
        type="submit"
        variant="contained"
        color="secondary"
        sx={{ mt: 2, ml: 2 }}
      >
        Save Cart
      </Button>
    </form>
  );
};

export default CartEditor;
