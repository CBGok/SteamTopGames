import React, { useState, useEffect } from 'react';
import { Card, Container, Row, Col } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../App.css'; // CSS dosyasını ekleyin

const SteamGames = () => {
    const [games, setGames] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:7239/api/steamgames'); // Doğru URL'yi kullandığınızdan emin olun
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                console.log(data); // Veriyi kontrol etmek için konsola yazdır
                setGames(data);
            } catch (error) {
                console.error('Error fetching games:', error);
            }
        };

        fetchData();
    }, []);

    const handleCardClick = (appId) => {
        window.open(`https://store.steampowered.com/app/${appId}`, "_blank");
    };

    return (
        <Container>
            <h1 class="fun-title">Top 10 Most Played Steam Games</h1>
            <Row>
                {games.map((game, index) => (
                    <Col key={game.appId} md={4} className="mb-4">
                        <Card className="h-100" onClick={() => handleCardClick(game.appId)}>
                            <div className="card-number">{index + 1}</div>
                            <Card.Img variant="top" src={game.headerImage} alt={game.name} />
                            <Card.Body>
                                <Card.Title>{game.name}</Card.Title>
                                <Card.Text className="text-light">{game.description}</Card.Text>
                            </Card.Body>
                            <Card.Footer >
                                <small className="text-light">Current Players: {game.currentPlayers}</small>
                            </Card.Footer>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
    );
};

export default SteamGames;
