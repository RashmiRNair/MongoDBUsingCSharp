using System;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");
            //1. To insert simple model objects
            //db.InsertRecord("Users", new PersonModel { FirstName = "Mary", LastName = "Jones" });
           
            //2. To insert complex model objects
            //PersonModel person = new PersonModel()
            //{
            //    FirstName = "Joe",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel()
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "11100"
            //    }
            //};            
            //db.InsertRecord("Users", person);


            //3. To read records (Simpler to 'select * from db' in SQL database
            var records = db.LoadRecords<PersonModel>("Users");
            foreach (var item in records)
            {
                Console.WriteLine($"{item.Id}: {item.FirstName}: {item.LastName} ");
                if (item.PrimaryAddress != null)
                {
                    Console.WriteLine(item.PrimaryAddress.City);
                }
                Console.WriteLine();

            }


            //4. To read single record depending upon ID
            var userRecord  =  db.LoadRecordById<PersonModel>("Users", new Guid("8dcc7170-c154-407a-9a4e-f335c8fbd46e"));

            //5. To update the single record 
            userRecord.DateOfBirth =new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            db.UpsertRecord("Users", userRecord.Id, userRecord);

            //6. To delete record
            db.DeleteRecord<PersonModel>("Users", userRecord.Id);

            //7. To read data in different Model Object
            var nameRecords = db.LoadRecords<NameModel>("Users");
            foreach (var item in records)
            {
                Console.WriteLine($"{item.Id}: {item.FirstName}: {item.LastName} ");               
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }


    public class NameModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
