import { useCallback, useEffect, useState } from "react";
import {
  Button,
  Typography,
  Box,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
  SelectChangeEvent,
  TextField,
  Paper,
} from "@mui/material";
import { MerchantService } from "../../services/merchant.service";
import { GetMerchantsResponse } from "../../shared/models/merchant/Merchant";
import { useNavigate } from "react-router-dom";
import { Cookies } from "react-cookie";

const CheckoutPage: React.FC = () => {
  const [merchants, setMerchants] = useState<GetMerchantsResponse[]>([]);
  const [selectedMerchant, setSelectedMerchant] = useState<string>("");
  const [orderId, setOrderId] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const loadMerchants = async () => {
      try {
        const response = await MerchantService.getMerchants();
        console.log(response, response.data);
        if (response.status === 200) {
          setMerchants(response?.data ?? []);
        }
      } catch (error) {
        console.error("Error loading merchants:", error);
      }
    };

    void loadMerchants();
  }, []);

  const handleMerchantChange = useCallback(
    (event: SelectChangeEvent<string>) => {
      setSelectedMerchant(event.target.value as string);
    },
    [setSelectedMerchant]
  );

  const handleFetchOnClick = useCallback(async () => {
    const cookies = new Cookies();
    cookies.set(orderId, selectedMerchant);
    navigate(`/checkout/display-order/${orderId}`);
  }, [orderId, navigate, selectedMerchant]);

  return (
    <Paper sx={{ padding: 12 }}>
      <Box textAlign="center" mt={5}>
        <Typography variant="h4" gutterBottom>
          Svea Checkout Demo
        </Typography>
        <Box mb={3}>
          <Box>
            <FormControl variant="outlined" sx={{ m: 1, minWidth: 200 }}>
              <InputLabel id="merchant-select-label">
                Select Merchant
              </InputLabel>
              <Select
                labelId="merchant-select-label"
                id="merchant-select"
                value={selectedMerchant}
                onChange={handleMerchantChange}
                label="Select Merchant"
              >
                {merchants.map((merchant) => (
                  <MenuItem
                    key={merchant.merchantId}
                    value={merchant.merchantId}
                  >
                    {merchant.market}:{merchant.merchantId}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            <TextField
              onChange={(event) => {
                setOrderId(event.target.value);
              }}
              sx={{ m: 1 }}
            />
            <Button
              variant="contained"
              color="secondary"
              onClick={handleFetchOnClick}
              disabled={!selectedMerchant}
              sx={{ m: 1 }}
            >
              Fetch Existing Order
            </Button>
          </Box>
          <Box>
            <Typography variant="h6">
              OR
            </Typography>
            <Button
              variant="contained"
              color="secondary"
              onClick={() => {
                navigate("/checkout/create-order");
              }}
              sx={{ m: 1 }}
            >
              Create Order
            </Button>
          </Box>
        </Box>
        <Box mt={5}>
          <Typography variant="h5" gutterBottom>
            Part Payment Widget Demo
          </Typography>
          <Box className="svea-part-payment-widget" textAlign="center" mt={2}>
            <img
              src="https://cdn.svea.com/webpay/Svea_Primary_RGB_medium.png"
              alt="Svea"
              style={{
                width: "70px",
                marginRight: "14px",
                verticalAlign: "middle",
              }}
            />
            Delbetalning från{" "}
            <span>
              98&nbsp;<span>kr</span>
            </span>
            /månad
          </Box>
        </Box>
      </Box>
    </Paper>
  );
};

export default CheckoutPage;
