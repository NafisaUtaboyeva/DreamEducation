using DreamEducation.Domain.Commons;
using DreamEducation.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DreamEducation.Domain.Entities.Users
{
    public class User : IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+@[gmail|yandex]+.[a-zA-Z0-9-.]+$", ErrorMessage = "You entered an error email!")]
        [DataType(DataType.EmailAddress), Required(ErrorMessage = "You must enter an email!")]
        public string Email { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "You must enter password!")]
        public string Password { get; set; }
        public string Image { get; set; }


        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public ItemState State { get; set; }


        public void Create()
        {
            CreatedAt = DateTime.Now;
            State = ItemState.Created;
        }
        public void Update()
        {
            UpdatedAt = DateTime.Now;
            State = ItemState.Updated;
        }
        public void Delete()
        {
            DeletedAt = DateTime.Now;
            State = ItemState.Deleted;
        }

    }
}
