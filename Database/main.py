from fastapi import FastAPI, HTTPException, Depends
from fastapi.middleware.cors import CORSMiddleware
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, Session
from database import Base, Pokemon

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"], 
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# SQLAlchemy setup
SQLALCHEMY_DATABASE_URL = "sqlite:///./pokemonstreak.db"
engine = create_engine(SQLALCHEMY_DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base.metadata.create_all(bind=engine)

# Dependency to get database session
def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

# Function to create a new record in the database with the bestStreak value
def create_pokemon_streak(db, best_streak: int):
    db_pokemon_streak = Pokemon(bestStreak=best_streak)
    db.add(db_pokemon_streak)
    db.commit()
    db.refresh(db_pokemon_streak)
    return db_pokemon_streak

# Function to update the best streak value in the database
def update_best_streak(best_streak: int):
    db = SessionLocal()
    try:
        pokemon_streak = db.query(Pokemon).first()
        if pokemon_streak:
            pokemon_streak.bestStreak = best_streak
            db.commit()
    finally:
        db.close()

@app.post("/pokemon/streak/")
def save_best_streak(best_streak: int, db: Session = Depends(get_db)):
    update_best_streak(best_streak)  # Update the best streak in the database
    return create_pokemon_streak(db, best_streak)

@app.get("/pokemon/best-streak/")
def get_best_streak(db: Session = Depends(get_db)):
    pokemon_streak = db.query(Pokemon).first()
    if not pokemon_streak:
        return {"best_streak": "No data available"}
    return {"best_streak": pokemon_streak.bestStreak}
