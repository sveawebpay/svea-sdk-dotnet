﻿using Atata;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment;
using Sample.AspNetCore.SystemTests.PageObjectModels.Payment.Blocks;

namespace Sample.AspNetCore.SystemTests.PageObjectModels
{
    using _ = SveaPaymentFramePage;

    public class SveaPaymentFramePage : Page<_>
    {
        public EntityBlock<_> Entity { get; private set; }

        public DetailsB2BIdentificationBlock<_> B2BIdentification { get; private set; }

        public DetailsB2BAnonymousBlock<_> B2BAnonymous { get; private set; }

        public DetailsB2CIdentificationBlock<_> B2CIdentification { get; private set; }

        public InternationalIdentificationBlock<_> International { get; private set; }

        public DetailsB2CAnonymousBlock<_> B2CAnonymous { get; private set; }

        public AddShippingBlock<_> AddShippingBlock { get; private set; }

        public EditShippingBlock<_> EditShippingBlock { get; private set; }

        public SelectShippingBlock<_> SelectShippingBlock { get; private set; }

        public PaymentMethodsBlock<_> PaymentMethods { get; private set; }

        public InvoiceReferenceBlock<_> InvoiceReference { get; private set; }

		public LeasingPaymentBlock<_> Leasing { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByCss("button[data-testid='anonymous-toggle']")]
        public Button<_> AnonymousToggle { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeClick)]
        [FindByCss("button[data-testid='not-you-link']")]
        public Button<_> NotYou { get; private set; }

        [WaitSeconds(1, TriggerEvents.BeforeAndAfterClick)]
        [FindByCss("button[data-testid='submit-button']")]
        public Button<_> Submit { get; private set; }

        [FindByCss("div[data-testid='bankid-dialog']")]
        public Control<_> BankId{ get; private set; }

        [FindByCss("button[data-testid='confirm-button']")]
        public Button<_> ConfirmBankId { get; private set; }


        public Frame<CardPaymentFramePage, _> CardPaymentFramePage { get; set; }
    }
}