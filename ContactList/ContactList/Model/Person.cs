/*
 * Address Book
 *
 * HTL Programming Homework
 *
 * Author: Philipp Brandstetter
 * Date created: 09/30/2019
 */
using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ContactList.Model
{ 
    [DataContract]
    public partial class Person
    { 
        [Required]
        [DataMember(Name="id")]
        public int? Id { get; set; }

        [DataMember(Name="firstName")]
        public string FirstName { get; set; }

        [DataMember(Name="lastName")]
        public string LastName { get; set; }

        [Required]
        [DataMember(Name="email")]
        public string Email { get; set; }

    }
}
