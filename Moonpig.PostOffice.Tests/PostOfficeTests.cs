namespace Moonpig.PostOffice.Tests
{
    using System;
    using System.Collections.Generic;
    using Api.Controllers;
    using Shouldly;
    using Xunit;

    public class PostOfficeTests
    {
        [Fact]
        public void OneProductWithLeadTimeOfOneDay()
        {
            DateTime orderDate = new DateTime(2018, 1, 1);
            DateTime dispatchDate = new DateTime(2018, 1, 2);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() {1}, orderDate);
            date.Date.Date.ShouldBe(dispatchDate);
        }

        [Fact]
        public void OneProductWithLeadTimeOfTwoDay()
        {
            DateTime orderDate = new DateTime(2018, 1, 1);
            DateTime dispatchDate = new DateTime(2018, 1, 3);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() { 2 }, orderDate);
            date.Date.Date.ShouldBe(dispatchDate);
        }

        [Fact]
        public void OneProductWithLeadTimeOfThreeDay()
        {
            DateTime orderDate = new DateTime(2018, 1, 1);
            DateTime dispatchDate = new DateTime(2018, 1, 4);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() { 3 }, orderDate);
            date.Date.Date.ShouldBe(dispatchDate);
        }

        [Fact]
        public void TwoProductWithMaxLeadTimeOfTwoDay()
        {
            DateTime orderDate = new DateTime(2018, 1, 1);
            DateTime dispatchDate = new DateTime(2018, 1, 3);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() { 1, 2 }, orderDate);
            date.Date.Date.ShouldBe(dispatchDate);
        }

        [Fact]
        public void FridayHasExtraTwoDays()
        {
            DateTime orderDate = new DateTime(2018, 1, 5);
            DateTime dispatchDate = new DateTime(2018, 1, 8);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() { 1 }, orderDate);
            date.Date.ShouldBe(dispatchDate);
        }

        [Fact]
        public void SaturdayHasExtraThreeDays()
        {
            DateTime orderDate = new DateTime(2018, 1, 6);
            DateTime dispatchDate = new DateTime(2018, 1, 9);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() { 1 }, orderDate);
            date.Date.ShouldBe(dispatchDate);
        }

        [Fact]
        public void SundayHasExtraTwoDays()
        {
            DateTime orderDate = new DateTime(2018, 1, 7);
            DateTime dispatchDate = new DateTime(2018, 1, 9);

            DispatchDateController controller = new DispatchDateController();
            var date = controller.GetDispatchDate(new List<int>() { 1 }, orderDate);
            date.Date.ShouldBe(dispatchDate);
        }
    }
}
