using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YrsForMembership : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == (byte) MembershipTypeNames.Unknown || customer.MembershipTypeId == (byte) MembershipTypeNames.PayAsYouGo)
                return ValidationResult.Success;
            if (customer.DateOfBirth == null)
            {
                return new ValidationResult("The Date of Birth field is required");
            }
            var age = DateTime.Now.Year - customer.DateOfBirth.Value.Year;
            return (age > 18) ? 
                    ValidationResult.Success : 
                    new ValidationResult("Customer should be of 18 years of age for a membership.");
        }
    }
}