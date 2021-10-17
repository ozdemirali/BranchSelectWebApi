# BranchSelectWebApi
This proje is created for the vocational high school branch selection as Web Api and Mvc from Asp.Net 

## Target framework
* .Net Framework 4.7.2

## Below are the frameworks used.
* EntityFramework v6.4.4
* ExelDataReader  v3.6.0
* ExelDataReader.DataSet v3.6.0
* Microsoft.Owin v4.2.0
* Microsoft.Owin.Host.SystemWeb v4.2.0
* Microsoft.Owin.Security v4.2.0
* Microsoft.Owin.Security.OAuth v4.2.0
* Newtonsoft.json v12.0.2

## Database
* Microsoft Sql Server is used.
* In Web.config file, The Data source shown in the picture below should be your Sql Server Name.

![Web_Config](https://user-images.githubusercontent.com/20681737/126876992-ee07a2cb-730a-484f-992f-c967b795bf8b.PNG)




### Controller
There are 3 controller
* BranchController
* SchoolController 
* StudentController

### Authentication
* You must login first to use methods in this project. 
* You must get token for login. Token Role-Based Authentication is used.
* You can test on Postman above

* For Authentication
![Authentication](https://user-images.githubusercontent.com/20681737/126375245-5419fc3c-f119-4c5a-85dd-d385840df9b3.PNG)

### BranchController
Two method is user for this controller. You can test on Postman above

* First Method
![GetBranch](https://user-images.githubusercontent.com/20681737/126376219-3a51ea81-aa4c-471f-8d7a-0daa40795bda.PNG)

* Second Method
![CrateClasses](https://user-images.githubusercontent.com/20681737/134074167-5eac32d9-4385-4a0b-b9aa-3473dc27bc03.PNG)

### SchoolController
Two method is user for this controller. You can test on Postman above

* First Method
![SchoolGet](https://user-images.githubusercontent.com/20681737/132139376-eb5ce104-c71b-4438-8ac7-37e9ae248876.PNG)
 
 * Second Method
![SchoolPostHeader](https://user-images.githubusercontent.com/20681737/126375232-cac37953-b31a-422d-bd3f-359b93857c1f.PNG)
![SchoolPostBody](https://user-images.githubusercontent.com/20681737/132139382-12a806a8-4926-42f1-8ec5-d8404da2f114.PNG)

* Third Method
![SchoolGetRole](https://user-images.githubusercontent.com/20681737/131574067-7d995f0b-0ca1-4e91-afea-2744a049c75f.PNG)

* Fourth Method
![SchoolUploadHeader](https://user-images.githubusercontent.com/20681737/126375237-bf9ba07d-aea7-4294-b785-e0f35423c816.PNG)
![SchoolUploadBody](https://user-images.githubusercontent.com/20681737/126375234-ffad2c8f-7282-42ed-8465-5c34c01a6192.PNG)

Excel File should be like above.  Column Name can be different.

![Sample Excel File](https://user-images.githubusercontent.com/20681737/126504269-1be11e59-06bf-466d-8d8c-1ae8e8178c0d.PNG)


### StudentController
Five method is user for this controller. You can test on Postman above

* First Method
![GetId](https://user-images.githubusercontent.com/20681737/133906903-91c5a2c6-0e34-420f-872c-b1e6354d9d2b.PNG)
 
* Second Method
![GetAll](https://user-images.githubusercontent.com/20681737/133942259-dcda26a8-64e3-4ca9-8098-8939045b9ac6.PNG)

* Third Method
![GetResult](https://user-images.githubusercontent.com/20681737/133942258-d0778504-1b84-4b2a-87d8-cdeead3fccf6.PNG)

* Fourth Method
![GetBranchSelection](https://user-images.githubusercontent.com/20681737/126375250-0c0a8480-f2f4-4d19-b2bf-21d3772b71a1.PNG)

* Fifth Method
![GetBranchStatus](https://user-images.githubusercontent.com/20681737/132063579-c7e5ecd6-b2ec-4395-842d-b44ada4e505d.PNG)

* Sixth Method
![StudentPostHeader](https://user-images.githubusercontent.com/20681737/130363038-51f4202c-3685-427c-8bd9-db52c4532a18.PNG)
![StudentPostBody](https://user-images.githubusercontent.com/20681737/130363041-6b74ccfc-1b3b-4b1f-8c70-51cab6df70cd.PNG)

* Seventh
![StudentDelete](https://user-images.githubusercontent.com/20681737/134060814-860d8ea3-0212-44cd-9bf8-5a23ada3ae10.PNG)





