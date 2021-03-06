﻿using Lykke.BlockchainExplorer.BlockchainDataProvider.NBitcoinIndexer.Converters;
using NBitcoin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.BlockchainExplorer.BlockchainDataProvider.NBitcoinIndexer
{
    public class Serializer
    {
        public static void RegisterFrontConverters(JsonSerializerSettings settings, Network network = null)
        {
            settings.Converters.Add(new BitcoinSerializableJsonConverter());
            settings.Converters.Add(new MoneyJsonConverter());
            settings.Converters.Add(new CoinJsonConverter(network));
            settings.Converters.Add(new ScriptJsonConverter());
            settings.Converters.Add(new UInt160JsonConverter());
            settings.Converters.Add(new UInt256JsonConverter());
            settings.Converters.Add(new NetworkJsonConverter());
            settings.Converters.Add(new KeyPathJsonConverter());
            settings.Converters.Add(new Base58DataJsonConverter()
            {
                Network = network
            });
            settings.Converters.Add(new BalanceLocatorJsonConverter());
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static T ToObject<T>(string data)
        {
            return ToObject<T>(data, null);
        }
        public static T ToObject<T>(string data, Network network)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            RegisterFrontConverters(settings, network);
            return JsonConvert.DeserializeObject<T>(data, settings);
        }

        public static string ToString<T>(T response, Network network)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            RegisterFrontConverters(settings, network);
            return JsonConvert.SerializeObject(response, settings);
        }
        public static string ToString<T>(T response)
        {
            return ToString<T>(response, null);
        }
    }
}
