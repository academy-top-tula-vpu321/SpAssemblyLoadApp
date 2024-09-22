namespace SpAssamblyLibrary
{
    public class Employee
    {
        public string? Name {  get; set; }
        public int Age { set; get; }

        public Employee()
        {
            Name = "Anomim";
            Age = 0;
        }

        public Employee(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
