using System;


namespace TestLibrary
{
    public class TestClass
    {
        private string _user = "Nona Was Here!!!";

        public String UserName
        {
            get { return _user; }
            set { _user = value; }
        }

        public TestClass()
        {
            // Drazen joined the ctor
        }

        private void NewFunction()
        {
            var username = "Hello World";
        }
    }
}
