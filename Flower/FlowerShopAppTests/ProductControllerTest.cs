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

namespace FlowerShopAppTests
{
    public class ProductControllerTest
    {
        [Fact]
        public async void Index_ShouldReturnProperListCountForGivenList()
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
        public async void Index_ShouldReturnEmptyModel()
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
    }
}
