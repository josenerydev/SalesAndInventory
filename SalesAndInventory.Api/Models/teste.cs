// Employee.cs

using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System;

namespace HR.Domain.Test
{
    public class Employee
    {
        public int EmpId { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Title { get; private set; }
        public string TitleOfCourtesy { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime HireDate { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public string Phone { get; private set; }
        public int? MgrId { get; private set; }
        public virtual Employee Manager { get; private set; }

        private Employee()
        { }

        public Employee(string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate, string address, string city, string country, string phone, string region = null, string postalCode = null, Employee manager = null)
        {
            SetLastName(lastName);
            SetFirstName(firstName);
            SetTitle(title);
            SetTitleOfCourtesy(titleOfCourtesy);
            SetBirthDate(birthDate);
            SetHireDate(hireDate);
            SetAddress(address);
            SetCity(city);
            SetCountry(country);
            SetPhone(phone);
            SetRegion(region);
            SetPostalCode(postalCode);
            SetManager(manager);
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName) || lastName.Length > 20)
                throw new ArgumentException("Last name must be between 1 and 20 characters.");

            LastName = lastName;
        }

        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName) || firstName.Length > 10)
                throw new ArgumentException("First name must be between 1 and 10 characters.");

            FirstName = firstName;
        }

        private void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title) || title.Length > 30)
                throw new ArgumentException("Title must be between 1 and 30 characters.");

            Title = title;
        }

        private void SetTitleOfCourtesy(string titleOfCourtesy)
        {
            if (string.IsNullOrEmpty(titleOfCourtesy) || titleOfCourtesy.Length > 25)
                throw new ArgumentException("Title of courtesy must be between 1 and 25 characters.");

            TitleOfCourtesy = titleOfCourtesy;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            if (birthDate.Date > DateTime.Now.Date)
                throw new ArgumentException("Birth date cannot be in the future.");

            BirthDate = birthDate;
        }

        private void SetHireDate(DateTime hireDate)
        {
            if (hireDate.Date < BirthDate.Date)
                throw new ArgumentException("Hire date cannot be before birth date.");

            HireDate = hireDate;
        }

        private void SetAddress(string address)
        {
            if (string.IsNullOrEmpty(address) || address.Length > 60)
                throw new ArgumentException("Address must be between 1 and 60 characters.");

            Address = address;
        }

        private void SetCity(string city)
        {
            if (string.IsNullOrEmpty(city) || city.Length > 15)
                throw new ArgumentException("City must be between 1 and 15 characters.");

            City = city;
        }

        private void SetCountry(string country)
        {
            if (string.IsNullOrEmpty(country) || country.Length > 15)
                throw new ArgumentException("Country must be between 1 and 15 characters.");

            Country = country;
        }

        private void SetPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length > 24)
                throw new ArgumentException("Phone must be between 1 and 24 characters.");

            Phone = phone;
        }

        private void SetRegion(string region)
        {
            if (region != null && region.Length > 15)
                throw new ArgumentException("Region must be between 1 and 15 characters or null.");

            Region = region;
        }

        private void SetPostalCode(string postalCode)
        {
            if (postalCode != null && postalCode.Length > 10)
                throw new ArgumentException("Postal code must be between 1 and 10 characters or null.");

            PostalCode = postalCode;
        }

        private void SetManager(Employee manager)
        {
            if (manager != null && manager.EmpId == EmpId)
                throw new ArgumentException("An employee cannot be their own manager.");

            Manager = manager;
            MgrId = manager?.EmpId;
        }
    }
}

//using System;
//using FluentValidation;

//namespace HR.Domain
//{
//    public class Employee
//    {
//        // ... propriedades da classe Employee

//        private Employee() { }

//        public Employee(string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate, string address, string city, string country, string phone, string region = null, string postalCode = null, Employee manager = null)
//        {
//            var validator = new EmployeeValidator();
//            validator.ValidateAndThrow(this);

//            LastName = lastName;
//            FirstName = firstName;
//            Title = title;
//            TitleOfCourtesy = titleOfCourtesy;
//            BirthDate = birthDate;
//            HireDate = hireDate;
//            Address = address;
//            City = city;
//            Country = country;
//            Phone = phone;
//            Region = region;
//            PostalCode = postalCode;
//            Manager = manager;
//            MgrId = manager?.EmpId;
//        }
//    }
//}

//using System;
//using FluentValidation;

//namespace HR.Domain
//{
//    public class Employee
//    {
//        // ... propriedades da classe Employee

//        private Employee() { }

//        public Employee(string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate, string address, string city, string country, string phone, string region = null, string postalCode = null, Employee manager = null)
//        {
//            var validator = new EmployeeValidator();
//            validator.ValidateAndThrow(this);

//            LastName = lastName;
//            FirstName = firstName;
//            Title = title;
//            TitleOfCourtesy = titleOfCourtesy;
//            BirthDate = birthDate;
//            HireDate = hireDate;
//            Address = address;
//            City = city;
//            Country = country;
//            Phone = phone;
//            Region = region;
//            PostalCode = postalCode;
//            Manager = manager;
//            MgrId = manager?.EmpId;
//        }
//    }
//}

//public class Employee
//{
//    // ... Propriedades de Employee

//    // Método para promover um funcionário
//    public void Promote(string newTitle, Employee newManager)
//    {
//        if (string.IsNullOrEmpty(newTitle))
//        {
//            throw new ArgumentException("O novo título não pode ser nulo ou vazio.", nameof(newTitle));
//        }

//        if (newManager == null)
//        {
//            throw new ArgumentNullException(nameof(newManager), "O novo gerente não pode ser nulo.");
//        }

//        if (EmpId == newManager.EmpId)
//        {
//            throw new InvalidOperationException("Um funcionário não pode ser gerente de si mesmo.");
//        }

//        Title = newTitle;
//        SetManager(newManager);
//    }

//    // Método para definir o gerente de um funcionário
//    public void SetManager(Employee manager)
//    {
//        if (manager == null)
//        {
//            throw new ArgumentNullException(nameof(manager), "O gerente não pode ser nulo.");
//        }

//        if (EmpId == manager.EmpId)
//        {
//            throw new InvalidOperationException("Um funcionário não pode ser gerente de si mesmo.");
//        }

//        Manager = manager;
//        MgrId = manager.EmpId;
//    }

//    // ... Outros métodos e construtor
//}

//As regras de validação e a lógica de negócio principal são conceitos distintos, embora relacionados, em uma aplicação.

//Regras de validação se concentram em garantir que as propriedades e os dados de um objeto sejam válidos e consistentes de acordo com as restrições do domínio. Por exemplo, verificar se uma string não está vazia, um número está dentro de um intervalo específico, ou uma data está no passado.

//Lógica de negócio principal, por outro lado, trata das operações e comportamentos específicos do domínio, como realizar cálculos, gerenciar relacionamentos entre entidades ou aplicar regras de negócio específicas.

//Vamos usar a classe Employee como exemplo e criar um validador EmployeeValidator usando FluentValidation para ilustrar a diferença.
//Neste exemplo, o EmployeeValidator define regras de validação para as propriedades da classe Employee, como garantir que o sobrenome e o nome não estejam vazios e estejam dentro de um determinado intervalo de caracteres, e que a data de nascimento seja menor ou igual à data atual.

//Por outro lado, a lógica de negócio principal para a classe Employee pode incluir métodos como Promote ou SetManager, que realizam operações e gerenciam relacionamentos específicos do domínio, como mostrado no exemplo anterior. Esses métodos implementam comportamentos específicos do domínio, enquanto as regras de validação garantem que as propriedades do objeto sejam válidas e consistentes.