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




##### Question Collection Chosen
I decided to use the Question Collection idea.

Class Name: VARCHAR
Class Number: VARCHAR/TINYINT
Institution: VARCHAR
Question Type: VARCHAR
Question: TEXT
Answer: TEXT
Author: VARCHAR
Date Created: DATE/TIMESTAMP
Rating: TINYINT

