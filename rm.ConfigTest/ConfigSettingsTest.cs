using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using rm.Config;

namespace rm.ConfigTest
{
    [TestFixture]
    public class ConfigSettingsTest
    {
        [Test]
        public void Default01()
        {
            Assert.AreEqual("Value1 (from app settings) (override)",
                ConfigurationManager.AppSettings["CustomConfig.Key1"]);
            Assert.AreEqual("Value1 (from config section) (override)",
                ((NameValueCollection)ConfigurationManager.GetSection("CustomConfig"))["Key1"]);

            Assert.AreEqual("Value2 (from app settings) (override)",
                ConfigurationManager.AppSettings["CustomConfig.Key2"]);
            Assert.AreEqual("Value2 (from config section) (override)",
                ((NameValueCollection)ConfigurationManager.GetSection("CustomConfig"))["Key2"]);

            Assert.AreEqual("Value3 (from app settings) (override)",
                ConfigurationManager.AppSettings["Key3"]);

            Assert.AreEqual((string)null,
                ConfigurationManager.AppSettings["key-does-not-exist"]);
        }
        [Test(Description = "From app settings")]
        public void GetByPrefix01()
        {
            Assert.AreEqual("Value1 (from app settings) (override)",
                ConfigSettings.GetByPrefix("CustomConfig")["Key1"]);
            Assert.AreEqual("Value2 (from app settings) (override)",
                ConfigSettings.GetByPrefix("CustomConfig")["Key2"]);
        }
        [Test]
        public void GetByPrefix02()
        {
            Assert.Throws<ArgumentException>(() => ConfigSettings.GetByPrefix(""));
        }
        [Test(Description = "From config section")]
        public void GetSection01()
        {
            Assert.AreEqual("Value1 (from config section) (override)",
                ConfigSettings.GetSection("CustomConfig")["Key1"]);
            Assert.AreEqual("Value2 (from config section) (override)",
                ConfigSettings.GetSection("CustomConfig")["Key2"]);
        }
        [Test]
        public void GetSection02()
        {
            Assert.AreEqual((string)null,
                ConfigSettings.GetSection("CustomConfig")["key-does-not-exist"]);
        }
        [Test]
        public void AppSettingsConfigSource01()
        {
            Assert.AreEqual("Value1 (from app settings) (override)",
                ConfigSettings.GetByPrefix("CustomConfig")["Key1"]);
            Assert.AreEqual("Value2 (from app settings) (override)",
                ConfigSettings.GetByPrefix("CustomConfig")["Key2"]);
        }
        [Test]
        public void CustomConfigSource01()
        {
            Assert.AreEqual("Value1 (from config section) (override)",
                ConfigSettings.GetSection("CustomConfig")["Key1"]);
            Assert.AreEqual("Value2 (from config section) (override)",
                ConfigSettings.GetSection("CustomConfig")["Key2"]);
        }
        [Test]
        public void InstancesCountCheck01()
        {
            var count = 100;
            Thread[] threads = new Thread[count * 2];
            for (int i = 0; i < count; i++)
            {
                threads[i] = new Thread(() => ConfigSettings.GetSection("CustomConfig"));
            }
            for (int i = count; i < count * 2; i++)
            {
                threads[i] = new Thread(() => ConfigSettings.GetByPrefix("CustomConfig"));
            }
            for (int i = 0; i < count * 2; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < count * 2; i++)
            {
                threads[i].Join();
            }
            Assert.AreEqual(1, ConfigSettings.ConfigSectionCache.Keys.Count);
            Assert.AreEqual(1, ConfigSettings.AppSettingsCache.Keys.Count);
        }
    }
}
