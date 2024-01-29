# Microsoft-Message-Queuing-Load-Test-Tool
Microsoft-Message-Queuing-Load-Test-Tool

Command line Test Tools

Requires .Net Framework 4.7 runtime

#1 - MSMQSendMessage

	Commandline arguments
		NumberOfQueues
		SendMessagesEveryXMilliseconds

	Example:
		Load test with 500 Test Queues and SendMessages every second

		MSMQSendMessage 500 1000 *> SendMessagelogfile.txt

#2 - MSMQReceiveMessage

	Commandline arguments
		NumberOfQueues
		ReceiveMessagesEveryXMilliseconds

	Example:
		Load test with 500 Test Queues and ReceiveMessages every 500 milliseconds

		MSMQReceiveMessage 500 500 *> ReceiveMessagelogfile.txt

#3 - ClearMSMQueues
          All messages sent to MSMQ are stored on the server until they are cleared.
          Clear messages between tests if you need to start with no messages in queue.

	Commandline arguments
		NumberOfQueues

	Example:
		Clear messages from 500 Test Queues

		ClearMSMQueues 500 *> ClearMessagelogfile.txt

#4 - DeleteMSMQueues
	Delete all test queues from the Server	

	Commandline arguments
		NumberOfQueues

	Example:
		Delete 500 Test Queues from Server

		DeleteMSMQueues 500 *> DeleteMessagelogfile.txt
