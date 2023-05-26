using WebApiInMemory.Models.Entities;
using WebApiInMemory.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiInMemory.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepo;

		public EmployeeController(IEmployeeRepository employeeRepo)
		{
			_employeeRepo = employeeRepo;
		}

		[HttpGet("getemployeelist")]
		public ActionResult<List<Employee>> GetEmployeeList()
		{
			var result = _employeeRepo.GetEmployeeList();
			return Ok(result);
		}

		[HttpPost("addemployee")]
		public IActionResult AddEmployee(Employee product)
		{
			try
			{
				var result = _employeeRepo.AddEmployee(product);
				return Ok(new { Status = "Success", Message = "Save Data Successfully!", Data = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Status = "Error", Message = ex.Message });
			}
		}

		[HttpDelete("deleteemployee")]
		public IActionResult DeleteEmployee(string employeeId)
		{
			try
			{
				var result = _employeeRepo.DeleteEmployee(employeeId);
				if (result)
					return Ok(new { Status = "Success", Message = "Delete Data Successfully!"});
				else
					return Ok(new { Status = "Error", Message = "Delete Data Error!" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Status = "Error", Message = ex.Message });
			}
		}
	}
}
