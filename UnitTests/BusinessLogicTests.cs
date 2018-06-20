using System;
using System.Linq;
using System.Collections.Generic;

using BusinessLogic;
using Entities.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BusinessLogicTests
    {
        [TestMethod]
        public void CanHandleAddItemToBasket()
        {
            //Arrange
            bool success = false;
            int stockCount = 0;
            IEnumerable<IBasketItem> basket = null;
            IStockItem stock = null;

            WorkFlow workFlow = new WorkFlow();

            //Act
            stock = workFlow.GetAllStock().Where(x => x.Name == "Chocolate").SingleOrDefault();
            stockCount = stock.Stock;

            success = workFlow.AddItemToBasket("buddin", "Chocolate", 3);
            basket = workFlow.GetUserBasket("buddin");

            stock = workFlow.GetAllStock().Where(x => x.Name == "Chocolate").SingleOrDefault();

            //Assert
            Assert.IsTrue(success);
            Assert.IsNotNull(basket);
            Assert.IsTrue(basket.Count() == 1);
            Assert.IsTrue(basket.SingleOrDefault().ItemId == stock.Id);
            Assert.IsTrue(basket.SingleOrDefault().Quantity == 3);
            Assert.IsTrue(stock.Stock == (stockCount - 3));
        }

        [TestMethod]
        public void CanHandleRemoveItemFromBasket()
        {
            //Arrange
            bool addSuccess = false;
            bool removeSuccess = false;
            int stockCount = 0;
            IEnumerable<IBasketItem> basket = null;
            IStockItem stock = null;

            WorkFlow workFlow = new WorkFlow();

            //Act
            stock = workFlow.GetAllStock().Where(x => x.Name == "Chocolate").SingleOrDefault();
            stockCount = stock.Stock;

            addSuccess = workFlow.AddItemToBasket("buddin", "Chocolate", 3);
            removeSuccess = workFlow.ClearUserBasket("buddin");
            basket = workFlow.GetUserBasket("buddin");

            stock = workFlow.GetAllStock().Where(x => x.Name == "Chocolate").SingleOrDefault();

            //Assert
            Assert.IsTrue(addSuccess);
            Assert.IsTrue(removeSuccess);
            Assert.IsNotNull(basket);
            Assert.IsTrue(basket.Count() == 0);
            Assert.IsTrue(stock.Stock == stockCount);
        }
    }
}