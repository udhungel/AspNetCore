using EmployeeManagement_AspNetCore.Models;

namespace EmployeeManagement_AspNetCore.Interface
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);

    }
}
