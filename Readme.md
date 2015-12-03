bulkWaterMark Expression Plug-in Test
========
This is a feasibility study for writing an expression plug-in for [bulkWaterMark](http://www.bulkWaterMark.com).
bulkWaterMark is a freeware batch watermarking tool for protecting photos. There are also paid versions of bulkWaterMark, but the free one supports also this plug-in implementation. I needed a solution for watermarking 10,000+ pictures with a few properties per image file name that was saved in an xml. Therefore I have searched the internet for solutions and found bulkWaterMark and its expression features. Unfortunately the plug-in interface or SDK for bulkWaterMark is not documented (yet?), therefore I trialed and errored and put together a solution to get the xml data into my image watermarks.

How to write plug-ins for bulkWaterMark
--------
The uploaded code is a simplification of my solution with a basic xml structure that gets loaded and read for each image. bulkWaterMark supports .NET assemblies as plug-ins for expressions. To implement a plug-in you need the following classes:

* XmlExpressionsPlugIn: This is an empty plug-in class that creates an expression context. It derives from `ExpressionsPlugIn<TContext>`, where TContext is XmlExpressionContext. You also need to define the PlugInInfoAttribute to set the name and the version of the plug-in, otherwise it cannot be found by bulkWaterMark.
* XmlExpressionContext: This is the context class of our plug-in that "manages" all other expressions within this context and provides data for them. I have put my XmlDocument loading stuff in this class, so it gets only loaded once. The singleton instance of this class can be accessed by every expression to get informations about the current image for example. My implementation loads an xml file named data.xml that is in the same directory as the plug-in dll.
* XmlExpression: Now this is the expression class with its data resolving logic. Type parameters are the context and the output value type. You just need to override the OnResolve method. In this method you will get all the informations about the current input image. The implemented code is just using the filename of the input image and queries the xml for an `<image>` tag with the current filename. If one is found, then it returns the element and returns its inner text.


Installation
--------
Just put the built assembly and the data file into the bin/plugins folder of your bulkWaterMark installation.


Hope this is useful! I really hope this API will get extended or documented well soon, because I think this bulkWaterMark app has some potential for using it in websites or for other data-driven image generation.
