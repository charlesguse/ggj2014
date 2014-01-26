http://ogmoxna.codeplex.com/documentation

Loading a Level

All the work of loading an Ogmo Editor project and level can be taken care of with a single line of code once the following criteria are met:

* You have the above mentioned libraries referenced in your project.
* You have added the Ogmo Editor level you want to load to your content project, and have set the importer and processor to Ogmo Editor Level Importer and Ogmo Editor Level Processor respectively.
* You have changed the Ogmo Editor Level Processor Project parameter to point the Ogmo Editor project associated with the level. This will be a file path relative to the level being loaded. So, if the project file and level file are in the same directory then simply enter the project file name.
* You have all required texture assets copied to the content project's folder (but NOT added to the content project!) in a path as determined by your project file settings. Information on how this is setup is available at the Ogmo Editor help page mentioned above.




protected void LoadContent()
{
    OgmoLevel ogmoLevel = this.Content.Load<OgmoLevel>("myOgmoLevel");
}