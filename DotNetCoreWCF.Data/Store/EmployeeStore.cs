using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DotNetCoreWCF.Contracts.Model.Employees;

namespace DotNetCoreWCF.Data.Store
{
	public interface IEmployeeStore
	{
		Employee Add(Employee employee);
		Employee Get(int employeeId);
		IEnumerable<Employee> Get(EmployeeRequest request);
		Employee Delete(int employeeId);
	}

	public class EmployeeStore : IEmployeeStore
	{
		private LockDictionary<int, Employee> _employees = new LockDictionary<int, Employee>();
		private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
		private int _nextId = 0;

		public EmployeeStore()
		{
			Populate();
		}

		public Employee Add(Employee employee)
		{
			employee.EmployeeId = GetNextId();

			if (!_employees.TryAdd(employee.EmployeeId.Value, employee))
				_employees.TryGetValue(employee.EmployeeId.Value, out employee);

			return employee;
		}

		public Employee Delete(int employeeId)
		{
			if (_employees.TryGetValue(employeeId, out Employee employee))
				_employees.TryRemove(employeeId);

			return employee;
		}

		public Employee Get(int employeeId)
		{
			_employees.TryGetValue(employeeId, out Employee employee);
			return employee;
		}

		public IEnumerable<Employee> Get(EmployeeRequest request)
		{
			var employees = _employees
				.Values
				.AsQueryable();

			return employees
				.ById(request.EmployeeId)
				.ByFirstName(request.FirstName)
				.ByLastName(request.LastName)
				.ByIsActive(request.ActiveOnly)
				.ToArray();
		}

		private int GetNextId()
		{
			_locker.EnterUpgradeableReadLock();

			_locker.EnterWriteLock();
			try
			{
				_nextId++;
			}
			finally
			{
				_locker.ExitWriteLock();
			}
			_locker.ExitUpgradeableReadLock();

			return _nextId;
		}

		private void Populate()
		{
			int employeeId = 1;
			_employees.AddOrUpdate(employeeId, new Employee
			{
				EmployeeId = employeeId++,
				IsActive = true,
				EmailAddress = "joe@shmoe.com",
				FirstName = "Joe",
				LastName = "Shmoe",
				MiddleInitial = "M",
				PhoneNumber = "801-123-4567",
				Title = "Burgermeister-meister",
				UserName = "j.shmoe"
			});
			_employees.AddOrUpdate(employeeId, new Employee
			{
				EmployeeId = employeeId++,
				IsActive = true,
				EmailAddress = "brim@jickman.com",
				FirstName = "Brim",
				LastName = "Jickman",
				MiddleInitial = "J",
				PhoneNumber = "801-444-5555",
				Title = "Pianoman",
				UserName = "pianoman"
			});
			_employees.AddOrUpdate(employeeId, new Employee
			{
				EmployeeId = employeeId++,
				IsActive = true,
				EmailAddress = "jimmy@sausage.com",
				FirstName = "Jimmy",
				LastName = "Dean",
				MiddleInitial = "S",
				PhoneNumber = "801-123-9876",
				Title = "Sausage Shredder",
				UserName = "spicysausage"
			});

			_nextId = employeeId;
		}
	}

	public static class EmployeeExtensions
	{
		public static IQueryable<Employee> ById(this IQueryable<Employee> employees, int? id)
		{
			if (id.HasValue)
				employees = employees.Where(employee => employee.EmployeeId == id);

			return employees;
		}

		public static IQueryable<Employee> ByIsActive(this IQueryable<Employee> employees, bool? activeOnly)
		{
			if (activeOnly.HasValue)
				employees = employees.Where(employee => employee.IsActive);

			return employees;
		}

		public static IQueryable<Employee> ByFirstName(this IQueryable<Employee> employees, string firstName)
		{
			if (!string.IsNullOrWhiteSpace(firstName))
				employees = employees.Where(employee => string.Compare(employee.FirstName, firstName, true) == 0);

			return employees;
		}

		public static IQueryable<Employee> ByLastName(this IQueryable<Employee> employees, string lastName)
		{
			if (!string.IsNullOrWhiteSpace(lastName))
				employees = employees.Where(employee => string.Compare(employee.LastName, lastName, true) == 0);

			return employees;
		}
	}
}