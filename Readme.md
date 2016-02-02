bulkWaterMark Expression Plug-in Test
========
This is a feasibility study for writing an expression plug-in for [bulkWaterMark](http://www.pmlabs-apps.com/bulkwatermark/) (or [German](http://www.pmlabs.de/bulkwatermark/)).
bulkWaterMark is a freeware to watermark photos in batch. There are also paid versions of bulkWaterMark, but the free one supports also this plug-in implementation too. I needed a solution to batch watermark 10,000+ pictures with a few properties per image file name that was saved in an xml. Therefore I have searched the internet for solutions and found bulkWaterMark and its expression features. With expressions you can put dynamic data into text watermarks to watermark photos with Exif tags like GPS coordinates or file creation time.

Unfortunately the plug-in interface or SDK for bulkWaterMark is not documented (yet?), therefore I trialed and errored and put together a solution to get the xml data into my image watermarks.

How to write plug-ins for bulkWaterMark
--------
The uploaded code is a simplification of my solution with a basic xml structure that gets loaded and read for each image. bulkWaterMark supports .NET assemblies as plug-ins for expressions. To implement a plug-in you need the following classes:

* XmlExpressionsPlugIn: This is an empty plug-in class that creates an expression context. It derives from `ExpressionsPlugIn<TContext>`, where TContext is XmlExpressionContext. You also need to define the PlugInInfoAttribute to set the name and the version of the plug-in, otherwise it cannot be found by bulkWaterMark.
* XmlExpressionContext: This is the context class of our plug-in that "manages" all other expressions within this context and provides data for them. I have put my XmlDocument loading stuff in this class, so it gets only loaded once. The singleton instance of this class can be accessed by every expression to get informations about the current image for example. My implementation loads an xml file named data.xml that is in the same directory as the plug-in dll.
* XmlExpression: Now this is the expression class with its data resolving logic. Type parameters are the context and the output value type. You just need to override the OnResolve method. In this method you will get all the informations about the current input image. The implemented code is just using the filename of the input image and queries the xml for an `<image>` tag with the current filename. If one is found, then it returns the element and returns its inner text.


Installation
--------
Just put the built assembly and the data file into the bin/plugins folder of your bulkWaterMark installation.

How to use it in bulkWaterMark
--------
Just open the Demo.bwm profile in bulkWaterMark. This watermark profile contains a text layer with the expression `{Xml.Xml}`. As soon as you watermark an image that is called 01.jpg or 02.jpg, the text layer then displays the text that I've defined in the xml file. Change the file names in the xml to test the plug-in with other images.

Problems
--------
I found out that ReSharper does not work for the PMlabs.GrfX.* assemblies. Seems like obfuscation is destroying the metadata of the .NET assemblies of the app.

What's up next
--------
I will contact the devs soon for support since I've found some other settings like ApiKey in the PlugInInfoAttribute class. Would be nice if this plug-in could be extended that it can fetch a data source via ODBC, define what should be selected and used in bulkWaterMark via a nice dialog within the profile editor.

Update Februar 2016: API will still take a while, since they are building they want to release some plug-ins first to test out the interfaces and possibilities of the framework. They are ok with API use, but do not support it at the moment.

Hope this is useful! I really wish this API will get extended or documented well soon, because I think this bulkWaterMark app has some potential for using it in websites or for other data-driven image generation.
