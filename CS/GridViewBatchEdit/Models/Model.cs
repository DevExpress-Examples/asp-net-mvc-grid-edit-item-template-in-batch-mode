using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Models {
    public class BatchEditRepository {
        public static List<GridDataItem> GridData {
            get {
                var key = "34FAA431-CF79-4869-9488-93F6AAE81263";
                var Session = HttpContext.Current.Session;
                if(Session[key] == null)
                    Session[key] = Enumerable.Range(0, 100).Select(i => new GridDataItem {
                        ID = i,
                        C1 = i % 2,
                        C2 = i * 0.5 % 3,
                        C3 = "C3 " + i,
                        C4 = i % 2 == 0,
                        C5 = new DateTime(2013 + i, 12, 16)
                    }).ToList();
                return (List<GridDataItem>)Session[key];
            }
        }
        public static GridDataItem InsertNewItem(GridDataItem postedItem) {
            var newItem = new GridDataItem() { ID = GridData.Count };
            LoadNewValues(newItem, postedItem);
            GridData.Add(newItem);
            return newItem;
        }
        public static GridDataItem UpdateItem(GridDataItem postedItem) {
            var editedItem = GridData.First(i => i.ID == postedItem.ID);
            LoadNewValues(editedItem, postedItem);
            return editedItem;
        }
        public static GridDataItem DeleteItem(int itemKey) {
            var item = GridData.First(i => i.ID == itemKey);
            GridData.Remove(item);
            return item;
        }
        protected static void LoadNewValues(GridDataItem newItem, GridDataItem postedItem) {
            newItem.C1 = postedItem.C1;
            newItem.C2 = postedItem.C2;
            newItem.C3 = postedItem.C3;
            newItem.C4 = postedItem.C4;
            newItem.C5 = postedItem.C5;
        }
    }

    public class GridDataItem {
        public int ID { get; set; }
        public int C1 { get; set; }
        public double C2 { get; set; }
        public string C3 { get; set; }
        public bool C4 { get; set; }
        public DateTime C5 { get; set; }
    }
}