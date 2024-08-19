import InputField from "../../../../components/form-components/InputField";

const ValidateCallbackErrorMessage = () => {
  return (
    <>
      <InputField
        id="validationErrorMessage"
        label="Validation error message"
        width="100%"
        textInputFormPath="validationCallback.errorMessage"
      />
    </>
  );
};

export default ValidateCallbackErrorMessage;
