using Xunit;
using FluentAssertions;
using Bogus;
using SalesAndInventory.Api.Models;
using System;
using System.Collections.Generic;

namespace SalesAndInventory.Api.Tests
{
    public class EmployeeTests
    {
        private readonly Faker _faker;
        private readonly Random _random;

        public EmployeeTests()
        {
            _faker = new Faker();
            _random = new Random();
        }

        private Employee CreateEmployee(int empId = 1)
        {
            string Truncate(string str, int maxLength)
            {
                return str.Length > maxLength ? str.Substring(0, maxLength) : str;
            }

            return new Employee(empId,
                Truncate(_faker.Name.LastName(), 20),
                Truncate(_faker.Name.FirstName(), 10),
                Truncate(_faker.Name.JobTitle(), 30),
                Truncate(_faker.Name.Prefix(), 25),
                _faker.Date.Past(30, DateTime.Now.AddYears(-30)),
                _faker.Date.Past(10),
                Truncate(_faker.Address.StreetAddress(), 60),
                Truncate(_faker.Address.City(), 15),
                _faker.Address.StateAbbr(),
                Truncate(_faker.Address.ZipCode(), 10),
                Truncate(_faker.Address.Country(), 15),
                Truncate(_faker.Phone.PhoneNumber(), 24),
                null, null, new List<Employee>());
        }

        [Fact]
        public void Promote_ShouldChangeTitleAndManager()
        {
            // Arrange
            var employee = CreateEmployee();
            var newManager = CreateEmployee(_random.Next(2, 100));
            var newTitle = _faker.Name.JobTitle();

            // Act
            employee.Promote(newTitle, newManager);

            // Assert
            employee.Title.Should().Be(newTitle);
            employee.Manager.Should().Be(newManager);
        }

        [Fact]
        public void Promote_ShouldThrowArgumentException_WhenNewTitleIsEmpty()
        {
            // Arrange
            var employee = CreateEmployee();
            var newManager = CreateEmployee(_random.Next(2, 100));

            // Act
            Action action = () => employee.Promote("", newManager);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("O novo título não pode ser nulo ou vazio. (Parameter 'newTitle')");
        }

        [Fact]
        public void Promote_ShouldThrowArgumentNullException_WhenNewManagerIsNull()
        {
            // Arrange
            var employee = CreateEmployee();
            var newTitle = _faker.Name.JobTitle();

            // Act
            Action action = () => employee.Promote(newTitle, null);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("O novo gerente não pode ser nulo. (Parameter 'newManager')");
        }

        [Fact]
        public void Promote_ShouldThrowInvalidOperationException_WhenEmployeeIsPromotedToBeTheirOwnManager()
        {
            // Arrange
            var employee = CreateEmployee();
            var newTitle = _faker.Name.JobTitle();

            // Act
            Action action = () => employee.Promote(newTitle, employee);

            // Assert
            action.Should().Throw<InvalidOperationException>().WithMessage("Um funcionário não pode ser gerente de si mesmo.");
        }
    }
}