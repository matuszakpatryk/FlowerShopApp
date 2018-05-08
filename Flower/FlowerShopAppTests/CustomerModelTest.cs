using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Flower.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace FlowerShopAppTests
{
    public class CustomerModelTest
    {
        private readonly ITestOutputHelper output;

        public CustomerModelTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ValidCustomerShouldBeCreated()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Test",
                Surname = "Test",
                Phone = "123-345-456",
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.True(valid);
        }

        [Fact]
        public void CustomerWithoutCapitalizedNameShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "andrzej",
                Surname = "Test",
                Phone = "123-345",
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void CustomerWithoutCapitalizedSurameShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Andrzej",
                Surname = "palka",
                Phone = "123-345",
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void CustomerWithEmailWithoutAtCharacterShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Test",
                Surname = "Test",
                Phone = "123-345-456",
                Email = "paxwp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithEmailWithoutCharactersAfterAtShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Test",
                Surname = "Test",
                Phone = "123-345-456",
                Email = "pax@",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithEmailWithoutCharactersBeforeAtShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Test",
                Surname = "Test",
                Phone = "123-345-456",
                Email = "@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithoutNameShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Surname = "Test",
                Phone = "123-345-456",
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithNullPhoneShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Andrzej",
                Surname = "Test",
                Phone = null,
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithToShortPhoneNumberShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Andrzej",
                Surname = "Test",
                Phone = "123-345",
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }

        [Fact]
        public void ModelWithPhoneNumberWithoutDashShouldReturnFalse()
        {
            //Arrange
            var customer = new Customer
            {
                Name = "Andrzej",
                Surname = "Test",
                Phone = "123456789",
                Email = "pax@wp.pl",
                Country = "Poland",
                City = "Malbork"
            };

            //Act
            var context = new ValidationContext(customer, null, null);
            var result = new List<ValidationResult>();

            //Assert
            var valid = Validator.TryValidateObject(customer, context, result, true);
            Assert.False(valid);
        }
    }
}
