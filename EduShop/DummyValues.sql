
--INSERT INTO [dbo].[CategorySet]
--           ([Name]
--           ,[Description])
--     VALUES
--           ('Operating Systems'
--           ,'An operating system (OS) is software that manages computer hardware and software resources and provides common services for computer programs.'),
--		   ('Anti-Malware software','Keeps your Pc safe and healthy'),
--		   ('Adobe','Adobe software'),
--		   ('Microsoft','Office related software'),
--		   ('Plugins','In computing, a plug-in (or add-in / addin, plugin, extension or add-on / addon) is a software component that adds a specific feature to an existing software application.')

--		   GO


INSERT INTO [dbo].[ProductSet]
           ([Name]
           ,[Price]
           ,[ShortDescription]
           ,[ImgUrl]
           ,[AverageRating]
           ,[Description]
           ,[Category])
     VALUES
           ('Windows 8 Pro',
           199,
           'Windows 8 - The latest Microsoft OS',
           '~/Images/windows8box.jpg',
           '2', 
		   'Windows 8 Pro Product Key Card is a physical product key card which contains the product key and link to use to upgrade your current operating system to Windows 8 Pro. This Product Key Card is Medialess, meaning that it does not include a DVD or CD for installation. You will download the program directly from Microsoft and use this product key to install and activate the software.
				 This is the physical key card which will be shipped to you. After installing Windows 8 Pro, you also have the option of upgrading to Windows 8.1 Pro in the Microsoft App store at no additional cost.',
           'Operating System'),
		   ('Mac OS',
		   399,
		   'The newest Mac OS',
		   '~/Images/MissingPic.png',
		   '3',
		   'Mac OS is a series of graphical user interface-based operating systems developed by Apple Inc. for their Macintosh line of computer systems.',
		   'Operating System'),
		   		   ('Windows 7 Premium',
		   '99',
		   'Windows 7 Home Premium SP1 64bit',
		   '~/Images/Windows7.jpg',
		   '0',
		   'Windows 7 Home Premium makes it easy to create a home network and share all of your favorite photos, videos, and music. You can even watch, pause, rewind, and record TV (a broadcast TV tuner may be required). Get the best entertainment experience with Windows 7 Home Premium. Do you use your PC for work, run Windows XP programs, or require enhanced security? Consider Windows 7 Professional.',
		   'Operating System'),

		   ('Visual Studio 2010',
		   '249',
		   'Microsoft Visual Studio 2010 Professional Edition',
		   '~/Images/VisualStudio2010.jpg',
		   '0',
		   'User Integrated Development EnvironmentDevelopment Platform SupporTesting Tools Microsoft Visual Studio 2010 Professional is the essential tool for individuals performing basic development tasks. It simplifies the creation, debugging, and deployment of applications on a variety of platforms including SharePoint and the Cloud. Visual Studio 2010 Professional comes with integrated support for test-driven development, as well as debugging tools that help ensure high-quality solutions. Academic C5E-00525 Complete Product DVD-ROM Microsoft Microsoft Corporation PC Software Visual Studio 2010 Professional Visual Studio 2010 Professional Edition Windows www.microsoft.com',
		   'Microsoft'),

		   ('Microsoft Office',
		   '174',
		   'Microsoft Office Home and Business 2013 ',
		   '~/Images/Microsoft Office Home and Business 2013.jpg',
		   '0',
		   'Office Home & Business 2013 is designed to help you create and organize faster with time saving features and a clean, modern look. Plus, you can save your documents in the cloud on SkyDrive and access them when you are not at home.',
		   'Microsoft'),

		   ('Microsoft Word 2013',
		   '99',
		   'Microsoft Word 2013',
		   '~/Images/Microsoft Word 2013.png',
		   '0',
		   'Microsoft Office 2013 comes in twelve different editions, including three editions for retail outlets, two editions for volume licensing channel, five subscription-based editions available through Microsoft Office 365 program, the web application edition known as Office Web Apps and the Office RT edition made for tablets and mobile devices. Office Web Apps are available free of charge on the web although enterprises may obtain on-premises installations for a price.',
		   'Microsoft'),

		   ('Acrobat Professional',
		   '109',
		   'Acrobat Professional XI Student and Teacher Edition',
		   '~/Images/Acrobat Professional XI Student and Teacher Edition.jpg',
		   '0',
		   'Make your job easier every day with Adobe Acrobat XI Pro software. Quickly create PDF files or PDF Portfolios. Intuitively edit PDF files from within Acrobat or convert them to Microsoft Word, Excel, or PowerPoint formats. Build forms in minutes with the new, included Adobe FormsCentral desktop application. And use guided Actions to automate document preparation.',
		   'Adobe'),

		   ('Adobe Photoshop',
		   '143',
		   'Adobe Photoshop Elements 12 & Premiere Elements 12',
		   '~/Images/Adobe Photoshop Elements 12 & Premiere Elements 12.jpg',
		   '0',
		   'Empower your creative vision. Turn snapshots and video clips into spectacular photos and movies. Organize, edit, and make them look their best, and then share them in print, online, or as keepsakes. And now, take your Elements photos and videos with you, wherever you go, on your smartphone or tablet.',
		   'Adobe'),

		   ('Malwarebytes Anti-Malware',
		   '39',
		   'Malwarebytes Anti-Malware Lifetime',
		   '~/Images/Malwarebytes Anti-Malware Lifetime.jpg',
		   '0',
		   'Malwarebytes Anti-Malware is considered to be the next step in the detection and removal of malware. In our product we have compiled a number of new technologies that are designed to quickly detect, destroy, and prevent malware. Malwarebytes Anti-Malware can detect and remove malware that even the most well known anti-virus and anti-malware applications fail to detect. Malwarebytes Anti-Malware monitors every process and stops malicious processes before they even start. The Real-time Protection Module uses our advanced heuristic scanning technology which monitors your system to keep it safe and secure. In addition, we have implemented a threats center which will allow you to keep up to date with the latest malware threats.',
		   'Anti-Malware Software'),

		   ('Kaspersky Anti-Virus',
		   '29',
		   'Kaspersky Anti-Virus 2015 1 User',
		   '~/Images/Kaspersky Anti-Virus 2015 1 User.png',
		   '0',
		   'Essential PC Protection: Kaspersky Anti-Virus is the backbone of your PC’s antivirus security system, working behind the scenes to deliver the fastest and most trusted computer virus and spyware protection in the world.',
		   'Anti-Malware Software'),

		   ('Malwarebytes Anti-Malware',
		   '59',
		   'Malwarebytes Anti-Malware Premium Retail',
		   '~/Images/Malwarebytes Anti-Malware Premium Retail.jpg',
		   '0',
		   'Malwarebytes Anti-Malware Premium combines powerful new technologies designed to seek out, destroy, and prevent malware. It detects and protects in an easy-to-use, straightforward, heavy-hitting but lightweight anti-malware application.',
		   'Anti-Malware Software'),
		   
		   ('Focusrite Liquid Mix HD',
			'437',
			'Focusrite Liquid Mix HD TDM Plugin for Pro Tools HD',
			'~/Images/Focusrite Liquid Mix HD TDM Plugin for Pro Tools HD.jpg',
			'0',
			'Focusrites Liquid Mix HD offers the power of Liquid Mix exclusively to the Pro Tools HD market. It provides the same pool of classic EQ and compressor emulations as the Liquid Mix range and brings the sound of priceless and historically significant compressors and EQs straight to the DAW. Each instance of the Liquid Mix plug-in provides one Compressor and one EQ emulation from a huge selection of high-quality vintage and modern-day classics. Forty Compressors and twenty EQs are available right out of the box, and additional emulations are available free online. A hybrid user-defined seven-band "super EQ" is available in every instance, built from separate EQ bands. Also available is a "free" mode for all compressors, which upgrades original compressors with limited or absent controls to give them full features - yours to use as you choose! Liquid Mix HD has over 100 simultaneous mono instances which can be used on a standard HD3 system, and uses the same patented "Dynamic Convolution" process as Focusrites other Liquid products.',
			'Plugin')
			
		  
		   

GO

INSERT INTO [dbo].[UserSet]
           ([Email]
           ,[Password]
           ,[Firstname]
           ,[Lastname]
           ,[Address]
           ,[Zip]
           ,[City]
           ,[Phone]
           ,[IsAdmin])
     VALUES
           ('johnDoe@edushop.com',
           'qaz123',
           'John',
           'Doe',
           'The white house 1600 Pennsylvania Avenue NW',
           '20500',
           'Washington DC',
           '1202-456-1111',
           '1'),
		   ('jayleno@latenightshow.com',
		   '1234',
		   'Jay',
		   'Leno',
		   'Queens Street 2',
		   '20500',
		   'New York',
		   '555-2223-455',
		   '0')

GO

INSERT INTO [dbo].[ReviewSet]
           ([Title]
           ,[Content]
           ,[Rating]
           ,[DateAdded]
           ,[ProductId])
     VALUES
           ('A decent OS',
           'Windows 8 is a decent OS but i still prefer Windows 7',
           2.5,
           '2014-10-06',
           1),
		   ('Rubbish',
		   'Did not like it at all! Cant wait for the next Windows...',
		   0,
		   '2014-10-07',
		   1)

		   GO