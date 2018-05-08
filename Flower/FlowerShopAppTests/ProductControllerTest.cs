using System;
using System.Net.Http;
using System.Threading.Tasks;
using Flower.Controllers;
using Flower.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;
using Flower.Data.Repository.Interfaces;
using System.Collections.Generic;
using Flower.Models;
using FlowerShopAppTests.Mocks;
using System.Linq;

namespace FlowerShopAppTests
{
    public class ProductControllerTest
    {
        [Fact]
        public async void IndexShouldReturnProperListCountForGivenList()
        {
            // Arrange
            var items = new List<Product>
            {
                new Product(),
                new Product(),
                new Product()
            };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(items);
            var controller = new ProductController(mockRepository.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = (ViewResult)result;
            var model = (IList<Product>)viewResult.Model;
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async void IndexShouldReturnEmptyModel()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Product>());
            var controller = new ProductController(mockRepository.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = (ViewResult)result;
            var model = (IList<Product>)viewResult.Model;
            Assert.Empty(model);
        }

        [Fact]
        public async void CreateShouldReturnProperListSize()
        {
            // Arrange
            Product firstProduct = new Product();
            Product secondProduct = new Product();

            var mockRepository = new ProductRepositoryMock();
            var controller = new ProductController(mockRepository);

            // Act
            await controller.Create(firstProduct);
            await controller.Create(secondProduct);
            IEnumerable<Product> resultList = await mockRepository.GetAll();

            // Assert
            Assert.Equal(2, resultList.Count());
        }

        [Fact]
        public async Task EditCorrectUpdate()
        {
            //Arrange
            Product product = new Product
            {
                Name = "Eustoma",
            };

            var repositoryMock = new ProductRepositoryMock();
            var controller = new ProductController(repositoryMock);

            //Act
            await controller.Create(product);
            product.Name = "Amarylis";
            await controller.Edit(product.ProductID, product);

            //Assert
            Product clientResult = await repositoryMock.GetById(product.ProductID.ToString());
            string result = clientResult.Name;
            Assert.Equal("Amarylis", result);
        }

        [Fact]
        public async Task Edit_UpdateNonExistingClientThrowException()
        {
            //Arrange
            Product product = new Product();
            Product secondProduct = new Product();
            Product thirdProduct = new Product();

            var repositoryMock = new ProductRepositoryMock();
            var controller = new ProductController(repositoryMock);

            //Act
            await controller.Create(product);
            await controller.Create(secondProduct);
            thirdProduct.Name = "Amarylis";

            //Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => controller.Edit(thirdProduct.ProductID, thirdProduct));
        }

        //[Fact]
        public async Task Edit_GiveDiffrentIdAndClientReturnNotFoundView()
        {
            Product client1 = new Product
            {
                ProductID = 1
            };

            var repositoryMock = new Mock<IProductRepository>();

            var controller = new ProductController(repositoryMock.Object);
            var result = controller.Edit(6);
            var viewResult = await result as ViewResult;
            Assert.Equal("Error", viewResult.ViewName);
        }

        [Fact]
        public async void DeleteShouldRemoveProduct()
        {
            // Arrange
            Product firstProduct = new Product();
            Product secondProduct = new Product();

            var mockRepository = new ProductRepositoryMock();
            var controller = new ProductController(mockRepository);

            // Act
            await controller.Create(firstProduct);
            await controller.Create(secondProduct);
            await controller.DeleteConfirmed(firstProduct.ProductID);
            IEnumerable<Product> resultList = await mockRepository.GetAll();

            // Assert
            Assert.Equal(1, resultList.Count());
        }
    }
}
