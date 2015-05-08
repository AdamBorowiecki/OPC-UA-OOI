#CHARTER

##What is Object Oriented Internet

Today the Internet is used by millions of people to gain access to information resources and services. The widespread use of the HTTP and hypertext makes it possible to freely publish new information and expose it in a context of its description. Unfortunately, this is a human-centric environment that cannot easily be adapted for the application-centric approach, which is required to provide globally distributed enterprise management and real-time process control. In this project new architecture is to be reserched that can be used to provide a generic solution for publishing and updating information in a context that can be used to describe and discover it. It is accomplished by distributing publisher (server) tasks to three classes: 

1. information context management using object oriented programming paradigm, 
2. a predefined fixed set of services that are used to access the data and meta-date, and 
3. a pluggable custom data binding mechanism. 

It is proposed to implement this architecture using the [OPC Unified Architecture](http://goo.gl/y4EHUn) - a new emerging industrial integration standard that fulfills these architecture requirements. 

That makes the presented approach a real proposal for new technology wave based on the existing Internet infrastructure because it allows vendors to provide generic off-the-shelf products tested independently for interoperability

This project is aimed to workout deliverables supporting Process Data handling over Internet including but not limiting to:

•	Data Edition – UI allowing display and edition of any custom data

•	Data serialization and deserialization - see whitepaper: [Address Space Interchange XML](http://goo.gl/LE64MA)

•	Data binding – to define how the process data relate to the real world

•	Data Validation - see project [DataSerializationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/DataSerializationUnitTestProject)

•	Data Prototyping  - methods and tools to design custom data types

•	Data Discovery – methods and tools to find the data over the network.

In scope there are also deliverables supporting:

•	Exposition of the Process Data in the context of Metadata

•	Browsing (using the sematic) of the Metadata to selectively access requested Process Data

•	Modeling and representation of the Metadata - see whitepaper: [OPC UA Information Model Deployment] (http://goo.gl/HqYjvy)

•	Validation semantic and consistency of the Metadata - see project [UANodeSetValidationUnitTestProject](https://github.com/mpostol/OPC-UA-OOI/tree/master/SemanticDataSolution/USNodeSetValidationUnitTestProject)

##Out of scope

Out of scope is any work on exchanging the Process Data and Metadata over the network. The hope is that the interoperability can be gained as the result of usage of the OPC Unified Architecture international standard. 

##Conclusion

I hope it is a good place to prototype and converge the OPC UA communication technology with Semantic Data, Internet Of Things, Plug and Play, Global Data Discovery, Selective Availability, etc. concepts. My goal is to bridge a gap between OPC UA technology and Industrial IT Application Domains. 

Redd more:

[WIKI](https://github.com/mpostol/OPC-UA-OOI/wiki)

[My Blog](http://wwww.mpostol.wordpress.com/)

[About me](https://pl.linkedin.com/in/mpostol)

[OPC Foundation](https://opcfoundation.org/)
