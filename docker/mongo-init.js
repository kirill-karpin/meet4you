db.createUser(
        {
            user: 'meetforyou',
            pwd: 'meetforyou',
            roles: [
                {
                    role: 'readWrite',
                    db: 'meetforyou_files'
                }
            ]
        }
);

db = db.getSiblingDB('meetforyou_files');

db.createCollection('sample_collection');

db.sample_collection.insertMany([
 {
    name: 'William Shakespeare',
    birth_year: 1564,
    death_year: 1616
  },
  {
    name: 'George Orwell',
    birth_year: 1903,
    death_year: 1950
  },
  {
    name: 'Cory Doctorow',
    birth_year: 1971
  }  
]);
