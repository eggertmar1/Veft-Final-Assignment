## Final Assignment
### Author: Eggert Mar Eggertsson  <eggerte19@ru.is> 

Notification service does not work, the dockerfile is implemented and it runs, but I could not get the amqp listener connected. 


Images are sent to "tradebucked" in AWS S3. 
TradeRepository is not fully implemented, this is what works:  
``` Create trade request ```
``` Get trade request ```
``` Get trade by identifier ```

To run with docker: 
```docker-compose up --build```  
To run locally: 
```dotnet build``` -> ```dotnet run```


