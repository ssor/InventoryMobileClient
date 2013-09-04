using System;
using System.Collections.Generic;
using System.Text;
using rfidCheck;

namespace Inventory
{
    public delegate void deleControlInvoke(object o);

    public class RestUrl
    {
        public static string RestAddress = "http://" + sysConfig.ip + ":" + sysConfig.tcp_port + "/index.php/";
        //public static string RestAddress = "http://192.168.0.82:9002/index.php/";
        public static string addProduct = RestAddress + "Inventory/Inventory/addProduct";
        public static string updateProduct = RestAddress + "Inventory/Inventory/updateProduct";
        public static string deleteProduct = RestAddress + "Inventory/Inventory/deleteProduct";
        public static string allProducts = RestAddress + "Inventory/Inventory/getAllProducts";
        public static string getProduct = RestAddress + "Inventory/Inventory/getProduct";
        public static string addProductToStorage = RestAddress + "Inventory/Inventory/addProductToStorage";
        public static string getPreProListToStorage = RestAddress + "Inventory/Inventory/getPreProListToStorage";
        public static string deleteProductFromStorage = RestAddress + "Inventory/Inventory/deleteProductFromStorage";
        public static string getProductList4deleteProductFromStorage = RestAddress + "Inventory/Inventory/getProductList4deleteProductFromStorage";
        public static string getProductInfoForInventoryList = RestAddress + "Inventory/Inventory/getProductInfoForInventoryList";

        public static string addScanedTag = RestAddress + "RFIDReader/Reader/addScanTag";
        public static string addScanedTags = RestAddress + "RFIDReader/Reader/addScanTags";
        public static string getScanedTags = RestAddress + "RFIDReader/Reader/getScanTags";

        public static string getAllOrders = RestAddress + "Inventory/Order/getAllOrders";
        public static string addOrder = RestAddress + "Inventory/Order/addOrder";
        public static string deleteOrder = RestAddress + "Inventory/Order/deleteOrder";
        public static string deleteOrders = RestAddress + "Inventory/Order/deleteOrders";

        public static string allProductName = RestAddress + "Inventory/ProductName/getAllProductName";


        //public static string sendUDP = RestAddress + "Inventory/UdpInvoke/sendUDPCommand/cmd/1";

    }
}
