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
        public static string CompanyEmail => "company@svea.com";
        public static string SwedishFirstName => "Tess T";

        public static string SwedishLastName => "Persson";

        public static string LoremIpsum =>
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent convallis facilisis neque ut scelerisque. Morbi arcu purus, gravida sed velit nec, interdum egestas ante. Pellentesque dapibus nisl ultrices dolor placerat, eu lobortis mauris elementum. Curabitur placerat ante est. Fusce et massa est. Etiam quis lacus justo. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus nulla enim, ornare in facilisis quis, ornare nec erat. Nullam sit amet mi augue. Proin dignissim risus urna, sed pulvinar turpis sollicitudin quis. Proin pretium lacinia ullamcorper.";

        public static string OrganizationNumber => "5590326186";

        public static string SwedishPersonalNumber => "194605092222";

        public static string SwedishPersonalNumberShort => "4605092222";

        public static string SwedishPhoneNumber => "0706050403";

        public static string SwedishStreet => "Testgatan 1";
        
        public static string CareOf => "c/o Eriksson, Erik";

        public static string SwishPhoneNumber => "0811111111";

        public static string User => "admin";

        public static string SwedishZipCode => "99999";

        public static string SwedishCity => "Stan";

        public static string Reference => "Ref123";


        public static string NorwegianFirstName => "Ola";
        public static string NorwegianLastName => "Normann";
        public static string NorwegianPersonalNumber => "17054512066";
        public static string NorwegianStreet => "Testveien 2";
        public static string NorwegianZipCode=> "2066";
        public static string NorwegianCity => "Oslo";
        public static string NorwegianPhoneNumber => "17054512";

        
        public static string FinnishZipCode => "11111";
        public static string FinnishPhoneNumber => "43554343";
        public static string FinnishPersonalNumber => "290296-7808";
        
        
        public static string Description(int length = 30)
        {
            return LoremIpsum.Substring(0, length);
        }
    }
}