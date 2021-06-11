# ICS project

## Festival management application 
The resulting application is to be used for festival management.


## Phase 1 - object design
It is necessary to implement the object's structure for their subsequent implementation in the database.

### Data
Within the application, at least the following data will be required.

#### Music Band
- Original name
- Genre
- Photo
- Country of origin
- Long text description of the group
- Short description for the program
- List of performances at the festival

#### Stage
- Name
- Text description
- Photo
- List of groups that play on it

#### Festival program
Time slots for individual performances on individual stages:
- Music Band
- Stage
- Start time
- Finish time

## Phase 2 - database and business layer

On this phase business layer (BL) is created.
BL includes different types of models (detail model, list model) with corresponding mappers.
For communication with the database generic repository was created.
Additionally, repository uses Unit Of Work.
On the top of these components are Facades.
Release notes:

- Generic repository is added with Unit of Work
- Models for each entity
    - List model
    - Detail model
- Mappers between entities and models
- Facade for each entity
