using System;

namespace Sample.AspNetCore.SystemTests.Services
{
    public static class TestDataService
    {
        public static string CompanyName => "Authority AB";

        public static string CreditCardCvc => "210";

        public static string CreditCardExpiratioDate => DateTime.Now.AddMonths(3).ToString("MMyy");

        public static string CreditCardNumber => "5392127332010533";

        public static string Email => "tess.persson@svea.com";
        public static string FirstName => "Tess T";

        public static string LastName => "Persson";

        public static string LoremIpsum =>
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent convallis facilisis neque ut scelerisque. Morbi arcu purus, gravida sed velit nec, interdum egestas ante. Pellentesque dapibus nisl ultrices dolor placerat, eu lobortis mauris elementum. Curabitur placerat ante est. Fusce et massa est. Etiam quis lacus justo. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus nulla enim, ornare in facilisis quis, ornare nec erat. Nullam sit amet mi augue. Proin dignissim risus urna, sed pulvinar turpis sollicitudin quis. Proin pretium lacinia ullamcorper.";

        public static string OrganizationNumber => "4608142222";

        public static string Password => "Passw0rd!.";

        public static string PersonalNumber => "194605092222";

        public static string PersonalNumberShort => "4605092222";

        public static string PhoneNumber => "0706050403";

        public static string Street => "Testgatan 1";
        
        public static string CareOf => "c/o Eriksson, Erik";

        public static string SwishPhoneNumber => "0811111111";

        public static string User => "admin";

        public static string ZipCode => "99999";

        public static string City => "Stan";

        public static string Reference => "Ref123";


        public static string Description(int length = 30)
        {
            return LoremIpsum.Substring(0, length);
        }
    }
}