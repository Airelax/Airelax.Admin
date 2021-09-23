using System;
using System.Collections.Generic;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses;
using Airelax.Domain.Members.Defines;
using Airelax.Domain.Orders;
using Lazcat.Infrastructure.Common;

namespace Airelax.Domain.Members
{
    public class Member : AggregateRoot<string>
    {
        public Member()
        {
            Id = GuidHelper.CreateId(prefix: "M");
            IsDeleted = false;
            RegisterTime = DateTime.Now;
            IsPhoneVerified = false;
            IsEmailVerified = false;
            Gender = Gender.Other;
        }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string AddressDetail { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegisterTime { get; set; }
        public string Cover { get; set; }

        public ICollection<House> Houses { get; set; }
        public MemberLoginInfo MemberLoginInfo { get; set; }
        public MemberInfo MemberInfo { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Order> Orders { get; set; }

        // public void VerifyPhone()
        // {
        //     IsPhoneVerified = true;
        // }
        // public void VerifyEmail()
        // {
        //     IsEmailVerified = true;
        // }
    }
}