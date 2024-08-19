import React from "react";
import InputField from "../../../../components/form-components/InputField";

const ClientOrderNumber = () => {
  return (
    <InputField
      id="clientOrderNumberInput"
      label="Client order number"
      textInputFormPath="clientOrderNumber"
      width="100%"
    />
  );
};

export default ClientOrderNumber;
