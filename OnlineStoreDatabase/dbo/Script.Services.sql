/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO Service(Name, Price, Description, Image, ShortDescription) VALUES
('Slap', 14.99, 'Sometimes we all need a hard slap to the face to wake us up and convince us that we aren''t in some all consuming nightmare. Allow our trained technicans to bring you back to reality by causing you a substancial amount of pain.',
                '/images/slap.jpg', 'A hard slap to the face.'
            ),

			('Splash of Water', 29.99, 'Nothing wakes you up from the zombie-like state that you know as your everyday life like a bucket of cold water.',
               '/images/bucket.jpg','A bucket of cold water to the face.'
            ),('Puppies', 89.99, 'What could be better for restoring your spirits and bringing joy back to your heart than cuddling with an adorable puppy?','/images/puppies.jpg','Puppies!!!'
			)