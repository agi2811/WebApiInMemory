using AutoMapper;
using WebApiInMemory.Helpers;
using WebApiInMemory.Models;
using WebApiInMemory.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInMemory.Repository.Interface;
using Microsoft.IdentityModel.SecurityTokenService;

namespace WebApiInMemory.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private ApplicationDbContext _context;
		private readonly IMapper _mapper;

		public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public IEnumerable<Employee> GetEmployeeList()
		{
			return _context.Employee.ToList();
		}

		public Employee GetEmployeeById(string employeeId)
		{
			return GetEmployee(employeeId);
		}

		public Employee AddEmployee(Employee model)
		{
			try
			{
				// validate
				if (model == null)
					throw new AppException("Employee Data cannot null!");

				if (string.IsNullOrEmpty(model.EmployeeId))
					throw new AppException("Employee Id cannot empty!");

				if (string.IsNullOrEmpty(model.FullName))
					throw new AppException("Employee Name cannot empty!");

				if (model.BIrthDate == null)
					throw new AppException("BIrth Date cannot null!");

				if (_context.Employee.Any(x => x.EmployeeId == model.EmployeeId))
					throw new AppException("Employee Data '" + model.EmployeeId + "' is already taken!");

				// map model to new user object
				var employee = _mapper.Map<Employee>(model);

				// save user
				var result = _context.Employee.Add(employee);
				_context.SaveChanges();
				return result.Entity;
			}
			catch (Exception ex)
			{
				throw new BadRequestException(ex.Message);
			}
		}

		public bool DeleteEmployee(string employeeId)
		{
			var employee = _context.Employee.Where(x => x.EmployeeId.Equals(employeeId)).FirstOrDefault();
			var result = _context.Remove(employee);
			_context.SaveChanges();
			return result != null ? true : false;
		}

		// Helper Methods
		private Employee GetEmployee(string employeeId)
		{
			var user = _context.Employee.Find(employeeId);
			if (user == null) throw new KeyNotFoundException("Employee not found");
			return user;
		}	
	}
}
