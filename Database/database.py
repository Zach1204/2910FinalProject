from sqlalchemy import Column, Integer, String
from sqlalchemy.ext.declarative import declarative_base
from pydantic import BaseModel

# SQLAlchemy setup
Base = declarative_base()

# Database model for Pokémon
class Pokemon(Base):
    __tablename__ = 'streak'

    bestStreak = Column(Integer, primary_key=True, autoincrement=False)

    def __repr__(self):
        return f"<Pokemon Best Streak {self.bestStreak})>"

# This is similar to Microsoft.Data.Sqlite - we need an object to represent the database record
class PokemonCreate(BaseModel):
    streak: int

# Here, we set the orm flag so that a database record can be converted to a Pydantic model on a read
class PokemonRead(PokemonCreate):
    class Config:
        orm_mode = True
