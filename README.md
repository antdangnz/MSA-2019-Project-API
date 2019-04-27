# MSA-2019-Project-API
Repo for 2019 MSA Phase 1. Attempting the API Project

# Project Idea
For this Project, I need to come up with a CRUD API just like last year.

Some ideas to work with:
- A Book Database
- Bill Logger
- Notes app?
- Some sort of registry
- Diary 
- Question collection (For class and for random?)


I will be going with:  
Either  
- Book Database
- Question Collection

## Book Database

The API will contain data about what books you currently own, and what books you currently want, etc.

It will contain the information about the book in the database. 
i.e. Author, Name, Date published, Title, owned or not, and possibly a synopsis. Possibly add some sort of priority as well. If not-owned, give it a priority, if owned give it a rating

## Question Collection

This API will hold questions related to test/exam questions regarding a particular class, etc. 

To make it simple, it will only contain questions from UoA courses. Maybe add a field where it's all set as UoA.

It will contain information such as class name, class number, whether it's a test or exam or tutorial, the question, and possibly the answer. Question authors?

It could also contain ratings for reliability. (Rating out of 10?)

## Question Collection Chosen
I decided to use the Question Collection idea.

<!-- id int [primary key]
Class Name: VARCHAR
Class Number: VARCHAR/TINYINT
Institution: VARCHAR
Question Type: VARCHAR
Question: TEXT
Answer: TEXT
Author: VARCHAR
Date Created: DATE/TIMESTAMP
Rating: TINYINT -->

The query string for the SQL database:

```
CREATE TABLE [questions]
(
	id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	class_name VARCHAR(30),
	class_number VARCHAR(30),
	institution VARCHAR(50),
	question_type VARCHAR(30),
	question_text TEXT,
	answer TEXT,
	author VARCHAR(50),
	rating TINYINT,
	date_created DATE
);
```

## CRUD Actions

Below, are headings that explain the different CRUD actions that have/will be implemented.

### Create



### Read



### Update



### Delete



## Unit Testing

I will also need to add some Unit Testing to this project, just for the sake of practice. (There is no mention of having to do Unit Tests for this project in the README).
I will need to update this section once I've implemented some unit testing.