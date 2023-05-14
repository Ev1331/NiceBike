namespace Nicebike.Models
{
	public class Employee
	{
		public int idEmployee { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string jobtitle { get; set; }
        public string phone { get; set; }

        public Employee(int idEmployee, string name, string surname, string mail, string jobtitle, string phone)
		{
			this.idEmployee = idEmployee;
			this.name = name;
			this.surname = surname;
			this.mail = mail;
			this.jobtitle = jobtitle;
			this.phone = phone;
		}
	}
}

