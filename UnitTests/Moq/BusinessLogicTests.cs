using System;
using System.Linq;
using System.Collections.Generic;

using Entities.Interfaces;
using Persistence.Repositories;
using DataAccess.DataStores;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace UnitTests.Moq
{
    [TestClass]
    public class MoqTests
    {
        [TestMethod]
        public void CanHandleTempStockAdd()
        {
            //Arrange
            var mockStockItemsRepository = new Mock<IRepository<IStockItem>>(MockBehavior.Strict);
            var stockItems = new Dictionary<int, IStockItem>();
            
            StockItemStore stockItemStore;

            IStockItem stockItem1;
            IStockItem stockItem2;

            int countAfterAdd = 0;
            int countAfterRefresh = 0;

            IStockItem mockStockItem1 = Mock.Of<IStockItem>();

            mockStockItem1.Id = 1;
            mockStockItem1.Name = "Sweets";
            mockStockItem1.Description = "Candy";
            mockStockItem1.Stock = 1000;
            mockStockItem1.Price = 0.5;

            IStockItem mockStockItem2 = Mock.Of<IStockItem>();

            mockStockItem2.Id = 2;
            mockStockItem2.Name = "Crisp";
            mockStockItem2.Description = "Snack";
            mockStockItem2.Stock = 150;
            mockStockItem2.Price = 0.75;

            //Act
            stockItems.Add(mockStockItem1.Id, mockStockItem1);

            mockStockItemsRepository.Setup(x => x.GetAll()).Returns(stockItems.Values);

            stockItemStore = new StockItemStore(mockStockItemsRepository.Object, stockItems);
            stockItemStore.Add(mockStockItem2);

            countAfterAdd = stockItemStore.Count();

            stockItem1 = stockItemStore.Find(1);
            stockItem2 = stockItemStore.Find(2);

            stockItemStore.Refresh();

            countAfterRefresh = stockItemStore.Count();

            //Assert
            Assert.IsTrue(stockItem1.Name == "Sweets");
            Assert.IsTrue(stockItem2.Name == "Crisp");
            Assert.IsTrue(countAfterAdd == 2);

            Assert.IsTrue(countAfterRefresh == 1);
        }
    }
}