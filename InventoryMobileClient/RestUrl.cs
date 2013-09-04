using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory
{
    public delegate void deleControlInvoke(object o);

    public class RestUrl
    {
        public static string RestAddress = "http://localhost:9002/index.php/";
        public static string addProduct = RestAddress + "Inventory/addProduct";
        public static string updateProduct = RestAddress + "Inventory/updateProduct";
        public static string deleteProduct = RestAddress + "Inventory/deleteProduct";
        public static string allProducts = RestAddress + "Inventory/getAllProducts";
        public static string getProduct = RestAddress + "Inventory/getProduct";
        public static string allScanedTag = RestAddress + "Inventory/addScanTag";
        public static string getScanedTags = RestAddress + "Inventory/getScanTags";
        public static string addProductToStorage = RestAddress + "Inventory/addProductToStorage";
        public static string getPreProListToStorage = RestAddress + "Inventory/getPreProListToStorage";
        public static string deleteProductFromStorage = RestAddress + "Inventory/deleteProductFromStorage";
        public static string getProductList4deleteProductFromStorage = RestAddress + "Inventory/getProductList4deleteProductFromStorage";
        public static string getProductInfoForInventoryList = RestAddress + "Inventory/getProductInfoForInventoryList";

        public static string getAllOrders = RestAddress + "Order/getAllOrders";
        public static string addOrder = RestAddress + "Order/addOrder";
        public static string deleteOrder = RestAddress + "Order/deleteOrder";
        public static string deleteOrders = RestAddress + "Order/deleteOrders";

        public static string allProductName = RestAddress + "ProductName/getAllProductName";


        public static string sendUDP = RestAddress + "UdpInvoke/sendUDPCommand/cmd/1";

    }
}
