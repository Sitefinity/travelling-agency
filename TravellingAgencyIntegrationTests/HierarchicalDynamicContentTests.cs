using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace TravellingAgencyIntegrationTests
{
    [TestFixture]
    [Description("Integration tests for the hierarchical dynamic content of the Travelling Agency module.")]
    public class HierarchicalDynamicContentTests
    {
        #region Hierarchy tests

        [Test]
        [Category("SDK")]
        [Description("A test that asserts that Country type is the parent of the Festival type.")]
        [Author("SDK")]
        public void CountryIsParentOfFestival()
        {
            var dynamicModuleManager = DynamicModuleManager.GetManager();
            var festivalType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.FestivalType);
            var festival = dynamicModuleManager.CreateDataItem(festivalType);

            var isValidParent = DynamicContentExtensions.IsValidParent(festival, HierarchicalDynamicContentTests.CountryType);

            Assert.IsTrue(isValidParent);
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts that Country type is the parent of the City type.")]
        [Author("SDK")]
        public void CountryIsParentOfCity()
        {
            var dynamicModuleManager = DynamicModuleManager.GetManager();
            var cityType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CityType);
            var city = dynamicModuleManager.CreateDataItem(cityType);

            var isValidParent = DynamicContentExtensions.IsValidParent(city, HierarchicalDynamicContentTests.CountryType);

            Assert.IsTrue(isValidParent);
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts that City type is the parent of the City type.")]
        [Author("SDK")]
        public void CityIsParentOfHotel()
        {
            var dynamicModuleManager = DynamicModuleManager.GetManager();
            var hotelType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.HotelType);
            var hotel = dynamicModuleManager.CreateDataItem(hotelType);

            var isValidParent = DynamicContentExtensions.IsValidParent(hotel, HierarchicalDynamicContentTests.CityType);

            Assert.IsTrue(isValidParent);
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts that City type is the parent of the Restaurant type.")]
        [Author("SDK")]
        public void CityIsParentOfRestaurant()
        {
            var dynamicModuleManager = DynamicModuleManager.GetManager();
            var restaurantType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.RestaurantType);
            var restaurant = dynamicModuleManager.CreateDataItem(restaurantType);

            var isValidParent = DynamicContentExtensions.IsValidParent(restaurant, HierarchicalDynamicContentTests.CityType);

            Assert.IsTrue(isValidParent);
        }

        #endregion

        #region CRUD tests

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a city.")]
        [Author("SDK")]
        public void CreateCity()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type cityType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CityType);
            DynamicContent cityItem = dynamicModuleManager.CreateDataItem(cityType, HierarchicalDynamicContentTests.CityId, dynamicModuleManager.Provider.ApplicationName);

            cityItem.SetValue("Name", "Test Name");
            cityItem.SetValue("History", "Test History");
            Address location = new Address();
            CountryElement locationCountry = Config.Get<LocationsConfig>().Countries.Values.First(x => x.Name == "United States");
            location.CountryCode = locationCountry.IsoCode;
            location.StateCode = locationCountry.StatesProvinces.Values.First().Abbreviation;
            location.City = "Test City";
            location.Street = "Test Street";
            location.Zip = "12345";
            location.Latitude = 0.00;
            location.Longitude = 0.00;
            location.MapZoomLevel = 8;
            cityItem.SetValue("Location", location);

            LibrariesManager mainPictureManager = LibrariesManager.GetManager();
            var mainPictureItem = mainPictureManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            if (mainPictureItem != null)
            {
                cityItem.CreateRelation(mainPictureItem, "MainPicture");
            }

            cityItem.SetString("UrlName", "TestUrlName");
            cityItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            cityItem.SetValue("PublicationDate", DateTime.Now);

            cityItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            dynamicModuleManager.SaveChanges();

            var actualCity = dynamicModuleManager.GetDataItem(cityType, cityItem.Id);

            Assert.IsNotNull(actualCity);
            Assert.AreEqual(cityItem.GetValue("Name").ToString(), actualCity.GetValue("Name").ToString());
            Assert.AreEqual(cityItem.GetValue("History").ToString(), actualCity.GetValue("History").ToString());
            Assert.AreEqual(cityItem.GetValue("Location"), actualCity.GetValue("Location"));
            Assert.AreEqual(cityItem.GetValue("MainPicture"), actualCity.GetValue("MainPicture"));
            Assert.AreEqual(cityItem.GetValue("UrlName").ToString(), actualCity.GetValue("UrlName").ToString());
            Assert.AreEqual(cityItem.GetValue("Owner"), actualCity.GetValue("Owner"));
            Assert.AreEqual(cityItem.GetValue("PublicationDate"), actualCity.GetValue("PublicationDate"));
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a festival.")]
        [Author("SDK")]
        public void CreateFestival()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type festivalType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.FestivalType);
            DynamicContent festivalItem = dynamicModuleManager.CreateDataItem(festivalType, HierarchicalDynamicContentTests.FestivalId, dynamicModuleManager.Provider.ApplicationName);

            festivalItem.SetValue("Name", "Test Name");
            festivalItem.SetValue("Description", "Test Description");
            festivalItem.SetValue("From", DateTime.Now);
            festivalItem.SetValue("To", DateTime.Now);

            LibrariesManager mainPictureManager = LibrariesManager.GetManager();
            var mainPictureItem = mainPictureManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            if (mainPictureItem != null)
            {
                festivalItem.CreateRelation(mainPictureItem, "MainPicture");
            }

            Address address = new Address();
            CountryElement addressCountry = Config.Get<LocationsConfig>().Countries.Values.First(x => x.Name == "United States");
            address.CountryCode = addressCountry.IsoCode;
            address.StateCode = addressCountry.StatesProvinces.Values.First().Abbreviation;
            address.City = "Test City";
            address.Street = "Test Street";
            address.Zip = "12345";
            address.Latitude = 0.00;
            address.Longitude = 0.00;
            address.MapZoomLevel = 8;
            festivalItem.SetValue("Address", address);
            festivalItem.SetString("UrlName", "TestUrlName");
            festivalItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            festivalItem.SetValue("PublicationDate", DateTime.Now);
            festivalItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            dynamicModuleManager.SaveChanges();

            var actualFestival = dynamicModuleManager.GetDataItem(festivalType, festivalItem.Id);

            Assert.IsNotNull(actualFestival);
            Assert.AreEqual(festivalItem.GetValue("Name").ToString(), actualFestival.GetValue("Name").ToString());
            Assert.AreEqual(festivalItem.GetValue("Description").ToString(), actualFestival.GetValue("Description").ToString());
            Assert.AreEqual(festivalItem.GetValue("From"), actualFestival.GetValue("From"));
            Assert.AreEqual(festivalItem.GetValue("To"), actualFestival.GetValue("To"));
            Assert.AreEqual(festivalItem.GetValue("MainPicture"), actualFestival.GetValue("MainPicture"));
            Assert.AreEqual(festivalItem.GetValue("Address"), actualFestival.GetValue("Address"));
            Assert.AreEqual(festivalItem.GetValue("UrlName").ToString(), actualFestival.GetValue("UrlName").ToString());
            Assert.AreEqual(festivalItem.GetValue("Owner"), actualFestival.GetValue("Owner"));
            Assert.AreEqual(festivalItem.GetValue("PublicationDate"), actualFestival.GetValue("PublicationDate"));
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a restaurant.")]
        [Author("SDK")]
        public void CreateRestaurant()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type restaurantType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.RestaurantType);
            DynamicContent restaurantItem = dynamicModuleManager.CreateDataItem(restaurantType, HierarchicalDynamicContentTests.RestaurantId, dynamicModuleManager.Provider.ApplicationName);

            restaurantItem.SetValue("Name", "Test Name");
            restaurantItem.SetValue("Description", "Test Description");
            restaurantItem.SetValue("WorkingHours", "Test WorkingHours");

            LibrariesManager mainPictureManager = LibrariesManager.GetManager();
            var mainPictureItem = mainPictureManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            if (mainPictureItem != null)
            {
                restaurantItem.CreateRelation(mainPictureItem, "MainPicture");
            }

            Address location = new Address();
            CountryElement locationCountry = Config.Get<LocationsConfig>().Countries.Values.First(x => x.Name == "United States");
            location.CountryCode = locationCountry.IsoCode;
            location.StateCode = locationCountry.StatesProvinces.Values.First().Abbreviation;
            location.City = "Test City";
            location.Street = "Test Street";
            location.Zip = "12345";
            location.Latitude = 0.00;
            location.Longitude = 0.00;
            location.MapZoomLevel = 8;
            restaurantItem.SetValue("Location", location);
            restaurantItem.SetString("UrlName", "TestUrlName");
            restaurantItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            restaurantItem.SetValue("PublicationDate", DateTime.Now);
            restaurantItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            dynamicModuleManager.SaveChanges();

            var actualRestaurant = dynamicModuleManager.GetDataItem(restaurantType, restaurantItem.Id);

            Assert.IsNotNull(actualRestaurant);
            Assert.AreEqual(restaurantItem.GetValue("Name").ToString(), actualRestaurant.GetValue("Name").ToString());
            Assert.AreEqual(restaurantItem.GetValue("Description").ToString(), actualRestaurant.GetValue("Description").ToString());
            Assert.AreEqual(restaurantItem.GetValue("WorkingHours").ToString(), actualRestaurant.GetValue("WorkingHours").ToString());
            Assert.AreEqual(restaurantItem.GetValue("MainPicture"), actualRestaurant.GetValue("MainPicture"));
            Assert.AreEqual(restaurantItem.GetValue("Location"), actualRestaurant.GetValue("Location"));
            Assert.AreEqual(restaurantItem.GetValue("UrlName").ToString(), actualRestaurant.GetValue("UrlName").ToString());
            Assert.AreEqual(restaurantItem.GetValue("Owner"), actualRestaurant.GetValue("Owner"));
            Assert.AreEqual(restaurantItem.GetValue("PublicationDate"), actualRestaurant.GetValue("PublicationDate"));
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a hotel.")]
        [Author("SDK")]
        public void CreateHotel()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type hotelType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.HotelType);
            DynamicContent hotelItem = dynamicModuleManager.CreateDataItem(hotelType, HierarchicalDynamicContentTests.HotelId, dynamicModuleManager.Provider.ApplicationName);

            hotelItem.SetValue("Name", "Test Name");
            hotelItem.SetValue("Overview", "Test Overview");
            hotelItem.SetValue("Checkin", "Test Checkin");
            hotelItem.SetValue("Checkout", "Test Checkout");
            hotelItem.SetValue("FoodAndDrink", new string[] { "Option2" });
            hotelItem.SetValue("Activities", new string[] { "Option2" });
            hotelItem.SetValue("Rating", 25);

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var Tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "Tags").FirstOrDefault();
            if (Tag != null)
            {
                hotelItem.Organizer.AddTaxa("Tags", Tag.Id);
            }

            Address location = new Address();
            CountryElement locationCountry = Config.Get<LocationsConfig>().Countries.Values.First(x => x.Name == "United States");
            location.CountryCode = locationCountry.IsoCode;
            location.StateCode = locationCountry.StatesProvinces.Values.First().Abbreviation;
            location.City = "Test City";
            location.Street = "Test Street";
            location.Zip = "12345";
            location.Latitude = 0.00;
            location.Longitude = 0.00;
            location.MapZoomLevel = 8;
            hotelItem.SetValue("Location", location);

            LibrariesManager mainPictureManager = LibrariesManager.GetManager();
            var mainPictureItem = mainPictureManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            if (mainPictureItem != null)
            {
                hotelItem.CreateRelation(mainPictureItem, "MainPicture");
            }

            hotelItem.SetString("UrlName", "TestUrlName");
            hotelItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            hotelItem.SetValue("PublicationDate", DateTime.Now);

            hotelItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            dynamicModuleManager.SaveChanges();

            var actualHotel = dynamicModuleManager.GetDataItem(hotelType, hotelItem.Id);

            Assert.IsNotNull(actualHotel);
            Assert.AreEqual(hotelItem.GetValue("Name").ToString(), actualHotel.GetValue("Name").ToString());
            Assert.AreEqual(hotelItem.GetValue("Overview").ToString(), actualHotel.GetValue("Overview").ToString());
            Assert.AreEqual(hotelItem.GetValue("Checkin").ToString(), actualHotel.GetValue("Checkin").ToString());
            Assert.AreEqual(hotelItem.GetValue("Checkout").ToString(), actualHotel.GetValue("Checkout").ToString());
            Assert.AreEqual(hotelItem.GetValue("FoodAndDrink").ToString(), actualHotel.GetValue("FoodAndDrink").ToString());
            Assert.AreEqual(hotelItem.GetValue("Activities").ToString(), actualHotel.GetValue("Activities").ToString());
            Assert.AreEqual(hotelItem.GetValue("Rating"), actualHotel.GetValue("Rating"));
            Assert.AreEqual(hotelItem.GetValue("MainPicture"), actualHotel.GetValue("MainPicture"));
            Assert.AreEqual(hotelItem.GetValue("Location"), actualHotel.GetValue("Location"));
            Assert.AreEqual(hotelItem.GetValue("UrlName").ToString(), actualHotel.GetValue("UrlName").ToString());
            Assert.AreEqual(hotelItem.GetValue("Owner"), actualHotel.GetValue("Owner"));
            Assert.AreEqual(hotelItem.GetValue("PublicationDate"), actualHotel.GetValue("PublicationDate"));
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a country.")]
        [Author("SDK")]
        public void CreateCountry()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type countryType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CountryType);
            DynamicContent countryItem = dynamicModuleManager.CreateDataItem(countryType, HierarchicalDynamicContentTests.CountryId, dynamicModuleManager.Provider.ApplicationName);

            countryItem.SetValue("Name", "Test country");
            countryItem.SetValue("Description", "Test Description");

            Address location = new Address();
            location.Latitude = 0.00;
            location.Longitude = 0.00;
            location.MapZoomLevel = 8;
            countryItem.SetValue("Location", location);

            LibrariesManager mainPictureManager = LibrariesManager.GetManager();
            var mainPictureItem = mainPictureManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
            if (mainPictureItem != null)
            {
                countryItem.CreateRelation(mainPictureItem, "MainPicture");
            }

            countryItem.SetString("UrlName", "TestUrlName");
            countryItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            countryItem.SetValue("PublicationDate", DateTime.Now);
            countryItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            dynamicModuleManager.SaveChanges();

            var actualCountry = dynamicModuleManager.GetDataItem(countryType, countryItem.Id);
            Assert.IsNotNull(actualCountry);
            Assert.AreEqual(countryItem.GetValue("Name").ToString(), actualCountry.GetValue("Name").ToString());
            Assert.AreEqual(countryItem.GetValue("Description").ToString(), actualCountry.GetValue("Description").ToString());
            Assert.AreEqual(countryItem.GetValue("Location"), actualCountry.GetValue("Location"));
            Assert.AreEqual(countryItem.GetValue("MainPicture"), actualCountry.GetValue("MainPicture"));
            Assert.AreEqual(countryItem.GetValue("UrlName").ToString(), actualCountry.GetValue("UrlName").ToString());
            Assert.AreEqual(countryItem.GetValue("Owner"), actualCountry.GetValue("Owner"));
            Assert.AreEqual(countryItem.GetValue("PublicationDate"), actualCountry.GetValue("PublicationDate"));
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a country with city and festival child types.")]
        [Author("SDK")]
        public void CreateCountryWithFestivalAndCity()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type countryType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CountryType);
            DynamicContent countryItem = dynamicModuleManager.CreateDataItem(countryType, HierarchicalDynamicContentTests.CountryId, dynamicModuleManager.Provider.ApplicationName);

            countryItem.SetValue("Name", "Test country");
            countryItem.SetValue("Description", "Test Description");
            countryItem.SetString("UrlName", "TestUrlName");
            countryItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            countryItem.SetValue("PublicationDate", DateTime.Now);
            countryItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            Type cityType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CityType);
            DynamicContent cityItem = dynamicModuleManager.CreateDataItem(cityType, HierarchicalDynamicContentTests.CityId, dynamicModuleManager.Provider.ApplicationName);
            cityItem.SetValue("Name", "Test Name");
            cityItem.SetValue("History", "Test History");
            cityItem.SetString("UrlName", "TestUrlName");
            cityItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            cityItem.SetValue("PublicationDate", DateTime.Now);
            cityItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            countryItem.AddChildItem(cityItem);

            Type festivalType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.FestivalType);
            DynamicContent festivalItem = dynamicModuleManager.CreateDataItem(festivalType, HierarchicalDynamicContentTests.FestivalId, dynamicModuleManager.Provider.ApplicationName);
            festivalItem.SetValue("Name", "Test Name");
            festivalItem.SetValue("Description", "Test Description");
            festivalItem.SetValue("From", DateTime.Now);
            festivalItem.SetValue("To", DateTime.Now);
            festivalItem.SetString("UrlName", "TestUrlName");
            festivalItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            festivalItem.SetValue("PublicationDate", DateTime.Now);
            festivalItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            countryItem.AddChildItem(festivalItem);

            dynamicModuleManager.SaveChanges();

            var actualCountry = dynamicModuleManager.GetDataItem(countryType, countryItem.Id);

            Assert.IsNotNull(actualCountry);

            var cityCount = DynamicContentExtensions.GetChildItemsCount(actualCountry, HierarchicalDynamicContentTests.CityType);
            var festivalCount = DynamicContentExtensions.GetChildItemsCount(actualCountry, HierarchicalDynamicContentTests.FestivalType);

            Assert.AreEqual(cityCount, 1);
            Assert.AreEqual(festivalCount, 1);
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a city with hotel and restaurant child types.")]
        [Author("SDK")]
        public void CreateCityWithHotelAndRestaurant()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type cityType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CityType);
            DynamicContent cityItem = dynamicModuleManager.CreateDataItem(cityType, HierarchicalDynamicContentTests.CityId, dynamicModuleManager.Provider.ApplicationName);

            cityItem.SetValue("Name", "Test Name");
            cityItem.SetValue("History", "Test History");
            cityItem.SetString("UrlName", "TestUrlName");
            cityItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            cityItem.SetValue("PublicationDate", DateTime.Now);
            cityItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            Type hotelType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.HotelType);
            DynamicContent hotelItem = dynamicModuleManager.CreateDataItem(hotelType, HierarchicalDynamicContentTests.HotelId, dynamicModuleManager.Provider.ApplicationName);
            hotelItem.SetValue("Name", "Test Name");
            hotelItem.SetValue("Overview", "Test Overview");

            cityItem.AddChildItem(hotelItem);

            Type restaurantType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.RestaurantType);
            DynamicContent restaurantItem = dynamicModuleManager.CreateDataItem(restaurantType, HierarchicalDynamicContentTests.RestaurantId, dynamicModuleManager.Provider.ApplicationName);
            restaurantItem.SetValue("Name", "Test Name");
            restaurantItem.SetValue("Description", "Test Description");

            cityItem.AddChildItem(restaurantItem);

            dynamicModuleManager.SaveChanges();

            var actualCity = dynamicModuleManager.GetDataItem(cityType, cityItem.Id);

            Assert.IsNotNull(actualCity);

            var hotelCount = DynamicContentExtensions.GetChildItemsCount(actualCity, HierarchicalDynamicContentTests.HotelType);
            var restaurantCount = DynamicContentExtensions.GetChildItemsCount(actualCity, HierarchicalDynamicContentTests.RestaurantType);

            Assert.AreEqual(hotelCount, 1);
            Assert.AreEqual(restaurantCount, 1);
        }

        [Test]
        [Category("SDK")]
        [Description("A test that asserts the creation of a city with 10 hotel and 10 restaurant child types.")]
        [Author("SDK")]
        public void CreateCityWithTenHotelsAndTenRestaurant()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
            Type cityType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CityType);
            DynamicContent cityItem = dynamicModuleManager.CreateDataItem(cityType, HierarchicalDynamicContentTests.CityId, dynamicModuleManager.Provider.ApplicationName);

            cityItem.SetValue("Name", "Test Name");
            cityItem.SetValue("History", "Test History");
            cityItem.SetString("UrlName", "TestUrlName");
            cityItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            cityItem.SetValue("PublicationDate", DateTime.Now);
            cityItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            Type hotelType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.HotelType);

            for (int i = 0; i < 10; i++)
            {
                DynamicContent hotelItem = dynamicModuleManager.CreateDataItem(hotelType);
                hotelItem.SetValue("Name", "Test Name " + i);
                hotelItem.SetValue("Overview", "Test Overview " + i);
                cityItem.AddChildItem(hotelItem);
            }

            Type restaurantType = TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.RestaurantType);

            for (int i = 0; i < 10; i++)
            {
                DynamicContent restaurantItem = dynamicModuleManager.CreateDataItem(restaurantType);
                restaurantItem.SetValue("Name", "Test Name " + i);
                restaurantItem.SetValue("Description", "Test Description " + i);
                cityItem.AddChildItem(restaurantItem);
            }

            dynamicModuleManager.SaveChanges();

            var actualCity = dynamicModuleManager.GetDataItem(cityType, cityItem.Id);

            Assert.IsNotNull(actualCity);

            var hotelCount = DynamicContentExtensions.GetChildItemsCount(actualCity, HierarchicalDynamicContentTests.HotelType);
            var restaurantCount = DynamicContentExtensions.GetChildItemsCount(actualCity, HierarchicalDynamicContentTests.RestaurantType);

            Assert.AreEqual(hotelCount, 10);
            Assert.AreEqual(restaurantCount, 10);
        }

        #endregion

        #region TearDown

        [TearDown]
        public void TearDown()
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();

            var hotel = dynamicModuleManager
                .GetDataItems(TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.HotelType))
                .FirstOrDefault(x => x.Id == HierarchicalDynamicContentTests.HotelId);

            var restaurant = dynamicModuleManager
               .GetDataItems(TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.RestaurantType))
               .FirstOrDefault(x => x.Id == HierarchicalDynamicContentTests.RestaurantId);

            var city = dynamicModuleManager
               .GetDataItems(TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CityType))
               .FirstOrDefault(x => x.Id == HierarchicalDynamicContentTests.CityId);

            var festival = dynamicModuleManager
               .GetDataItems(TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.FestivalType))
               .FirstOrDefault(x => x.Id == HierarchicalDynamicContentTests.FestivalId);

            var country = dynamicModuleManager
               .GetDataItems(TypeResolutionService.ResolveType(HierarchicalDynamicContentTests.CountryType))
               .FirstOrDefault(x => x.Id == HierarchicalDynamicContentTests.CountryId);

            if (hotel != null)
            {
                HierarchicalDynamicContentTests.DeleteDataItem(hotel, dynamicModuleManager);
            }

            if (restaurant != null)
            {
                HierarchicalDynamicContentTests.DeleteDataItem(restaurant, dynamicModuleManager);
            }

            if (festival != null)
            {
                HierarchicalDynamicContentTests.DeleteDataItem(festival, dynamicModuleManager);
            }

            if (city != null)
            {
                HierarchicalDynamicContentTests.DeleteCity(city, dynamicModuleManager);
            }

            if (country != null)
            {
                HierarchicalDynamicContentTests.DeleteCountry(country, dynamicModuleManager);
            }
        }

        private static void DeleteCountry(DynamicContent country, DynamicModuleManager dynamicModuleManager)
        {
            var cities = country.GetChildItems(HierarchicalDynamicContentTests.CityType);

            var festivals = country.GetChildItems(HierarchicalDynamicContentTests.FestivalType);

            if (festivals != null)
            {
                foreach (var festival in festivals)
                {
                    HierarchicalDynamicContentTests.DeleteDataItem(festival, dynamicModuleManager);
                }
            }

            if (cities != null)
            {
                foreach (var city in cities)
                {
                    HierarchicalDynamicContentTests.DeleteCity(city, dynamicModuleManager);
                }
            }

            HierarchicalDynamicContentTests.DeleteDataItem(country, dynamicModuleManager);
        }

        private static void DeleteCity(DynamicContent city, DynamicModuleManager dynamicModuleManager)
        {
            var restaurants = city.GetChildItems(HierarchicalDynamicContentTests.RestaurantType);

            var hotels = city.GetChildItems(HierarchicalDynamicContentTests.RestaurantType);

            if (hotels != null)
            {
                foreach (var hotel in hotels)
                {
                    HierarchicalDynamicContentTests.DeleteDataItem(hotel, dynamicModuleManager);
                }
            }

            if (restaurants != null)
            {
                foreach (var restaurant in restaurants)
                {
                    HierarchicalDynamicContentTests.DeleteDataItem(restaurant, dynamicModuleManager);
                }
            }

            HierarchicalDynamicContentTests.DeleteDataItem(city, dynamicModuleManager);
        }

        public static void DeleteDataItem(DynamicContent dataItem, DynamicModuleManager dynamicModuleManager)
        {
            dynamicModuleManager.DeleteDataItem(dataItem);
            dynamicModuleManager.SaveChanges();
        }

        public static void DeleteDataItem(Type itemType, Guid dataItemId, DynamicModuleManager dynamicModuleManager)
        {
            dynamicModuleManager.DeleteDataItem(itemType, dataItemId);
            dynamicModuleManager.SaveChanges();
        }

        #endregion

        #region Private members

        public static readonly string CountryType = "Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.Country";
        public static readonly string CityType = "Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.City";
        public static readonly string FestivalType = "Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.Festival";
        public static readonly string HotelType = "Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.Hotel";
        public static readonly string RestaurantType = "Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.Restaurant";

        public static readonly Guid CountryId = Guid.Parse("49C0F775-EC9F-4DA1-B89B-4A1FAC50F536");
        public static readonly Guid CityId = Guid.Parse("49C0F775-EC9F-4DA1-B89B-4A1FAC50F537");
        public static readonly Guid FestivalId = Guid.Parse("49C0F775-EC9F-4DA1-B89B-4A1FAC50F538");
        public static readonly Guid RestaurantId = Guid.Parse("49C0F775-EC9F-4DA1-B89B-4A1FAC50F539");
        public static readonly Guid HotelId = Guid.Parse("49C0F775-EC9F-4DA1-B89B-4A1FAC50F540");

        #endregion
    }
}