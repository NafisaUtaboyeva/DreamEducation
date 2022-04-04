using DreamEducation.Domain.Enums;
using System;

namespace DreamEducation.Domain.Commons
{
    internal interface IAuditable
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
        Guid? UpdatedBy { get; set; }
        ItemState State { get; set; }

        void Created();
        void Updated();
        void Deleted();
    }
}
