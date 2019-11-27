Documentation on thoughts:

I have tried to use as less time as possible for thinking about a good system for this. The system I tried to create is like a orchestrating system. I have thought more than I have actually coded. I would just like to explain my idea in a summary.

In this message routing system,
1.	the system considers that the message is already saved in database or a queue when the API request is sent to Transactional or Bulk API.
2.	in case of bulk messaging, the pipeline is divided into several independent stages. So that if message fails at any point, it can be reprocessed from the stage it was broken.
3.	assuming that transactional emails need immediate consideration, it has been immediately processed and send to its separate priority queue. In another thought, I would process transactional emails through the same bulk processing pipeline but only send it to separate priority queue. Clients can be given another API endpoint to check the status of the message they have sent.
4.	For every stage, micro-services can be created as per necessity to spread out the responsibility of the routing engine and make the system more scalable.
5.	I am guessing we already use message queues like RabbitMQ. I am not sure of it but I think it can be used instead of DB in client facing API to increase throughput.
6.	If Postgres DB is used, its structure can itself be partitioned by ClientId or something and would ease for multi-tenancy and make API more reliable.


