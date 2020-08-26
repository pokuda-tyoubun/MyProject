using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FxCommonLib.Utils;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class GoogleMapsApiUtilTest {
        [TestMethod]
        public void GetDistanceTest() {
            //2点間の距離を測定
            var distance = GoogleMapsApiUtil.GetDistance("東京", "大阪", "AIzaSyCbLZ8lYJkcmPQPZYRUoYCbLCI8KQNmc4k");
            //512Kmぐらい
            Assert.AreEqual(true, (distance > 500000));

            distance = GoogleMapsApiUtil.GetDistance("香川県東かがわ市帰来５５０－１", 
                                                            "大阪府大阪市北区中之島６丁目２−２７", 
                                                            "AIzaSyCbLZ8lYJkcmPQPZYRUoYCbLCI8KQNmc4k");
            //150Kmぐらい
            Assert.AreEqual(true, (distance > 150000));

        }
        [TestMethod]
        public void GetDurationTest() {
            //2点間の車での移動時間を測定
            var second = GoogleMapsApiUtil.GetDuration("東京", "大阪", 
                                                       GoogleMapsApiUtil.MovingMode.driving, 
                                                       "AIzaSyCbLZ8lYJkcmPQPZYRUoYCbLCI8KQNmc4k");
            //６時間ぐらい
            Assert.AreEqual(true, ((second / 3600) > 6));

            second = GoogleMapsApiUtil.GetDuration("香川県東かがわ市帰来５５０－１", 
                                                            "大阪府大阪市北区中之島６丁目２−２７", 
                                                            GoogleMapsApiUtil.MovingMode.driving, 
                                                            "AIzaSyCbLZ8lYJkcmPQPZYRUoYCbLCI8KQNmc4k");
            //２時間ぐらい
            Assert.AreEqual(true, ((second / 3600) > 2));

            //歩いた場合----------------------------------------------
            second = GoogleMapsApiUtil.GetDuration("香川県東かがわ市帰来５５０－１", 
                                                            "大阪府大阪市北区中之島６丁目２−２７", 
                                                            GoogleMapsApiUtil.MovingMode.walking, 
                                                            "AIzaSyCbLZ8lYJkcmPQPZYRUoYCbLCI8KQNmc4k");
            //２時間ぐらい
            Assert.AreEqual(true, ((second / 3600) > 2));

            //自転車の場合----------------------------------------------
            //second = GoogleMapsApiUtil.GetDuration("香川県東かがわ市帰来５５０－１", 
            //                                                "大阪府大阪市北区中之島６丁目２−２７", 
            //                                                GoogleMapsApiUtil.MovingMode.bicycling, 
            //                                                "AIzaSyCbLZ8lYJkcmPQPZYRUoYCbLCI8KQNmc4k");
            //２時間ぐらい
            //Assert.AreEqual(true, ((second / 3600) > 2));

        }
    }
}
