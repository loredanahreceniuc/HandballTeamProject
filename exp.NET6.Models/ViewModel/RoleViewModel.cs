using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Models.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateRoleViewModel
    {
        [Required]
        public string RoleId { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;
    }

    public class CreateRoleViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
