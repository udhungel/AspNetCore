namespace EmployeeManagement_AspNetCore.ViewModel
{
    public class EmployeeEditViewModel:EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string ExisitingPhotopath { get; set; }
    }
}
