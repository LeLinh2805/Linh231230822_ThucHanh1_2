using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ThucHanh1.Models
{
    public class Student
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        public string Name { get; set; } 

        [Required(ErrorMessage = "Email bắt buộc phải nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$",
            ErrorMessage = "Email phải có đuôi gmail.com")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "Mật khẩu bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu tối thiểu 8 ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string Password { get; set; } 

        [Required(ErrorMessage = "Ngành học không được để trống")]
        public Branch? Branch { get; set; } 

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public Gender? Gender { get; set; } 

        public bool IsRegular { get; set; }  

        [Required(ErrorMessage = "Địa chỉ bắt buộc phải nhập")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005",
            ErrorMessage = "Ngày sinh phải từ 01/01/1963 đến 31/12/2005")]
        [DataType(DataType.Date)]
        public DateTime DateOfBorth { get; set; }

       
        [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải từ 0.0 đến 10.0")]
        public double Score { get; set; } 
    }
}
