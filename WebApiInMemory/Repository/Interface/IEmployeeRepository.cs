using WebApiInMemory.Models.Entities;

namespace WebApiInMemory.Repository.Interface
{
	public interface IEmployeeRepository
	{
		IEnumerable<Employee> GetEmployeeList();
		Employee GetEmployeeById(string employeeId);
		Employee AddEmployee(Employee model);
		bool DeleteEmployee(string employeeId);
	}
}
