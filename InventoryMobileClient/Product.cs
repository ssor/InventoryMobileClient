using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory
{
    public class Product
    {
        public string productID;
        public string productName;
        public string produceDate;
        public string productCategory;
        public string descript;
        public string state;
        public Product()
        {
            this.productID = string.Empty;
            this.productName = string.Empty;
            this.produceDate = string.Empty;
            this.productCategory = string.Empty;
            this.descript = string.Empty;
        }
        public Product(string pID, string pName, string pDate, string pCategory, string pDescript)
        {
            this.productID = pID;
            this.productName = pName;
            this.produceDate = pDate;
            this.productCategory = pCategory;
            this.descript = pDescript;
            this.state = "new";
        }
        public string toString()
        {
            string strR = string.Empty;
            strR = string.Format("ID = {0}  Name = {1} category = {2}", this.productID, this.productName, this.descript);
            return strR;
        }
    }
}
