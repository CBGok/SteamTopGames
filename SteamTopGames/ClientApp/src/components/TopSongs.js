import React, { useState, useEffect } from 'react';
import { Card, Container, Row, Col } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import '../App.css'; // CSS dosyasını ekleyin

const TopSongs = () => {
    const [songs, setSongs] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch('https://localhost:7239/api/songs'); // Doğru URL'yi kullandığınızdan emin olun
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                console.log(data); // Veriyi kontrol etmek için konsola yazdır
                setSongs(data);
            } catch (error) {
                console.error('Error fetching songs:', error);
            }
        };

        fetchData();
    }, []);

    const handleCardClick = (url) => {
        window.open(url, "_blank");
    };

    return (
        <Container className="mt-4">
            <h1 className="fun-title">Top 10 Most Listened Songs</h1>
            <Row>
                {songs.map((song, index) => (
                    <Col key={index} md={4} className="mb-4">
                        <Card onClick={() => handleCardClick(song.url)}>
                            <div className="card-number">{index + 1}</div>
                            {song.imageUrl && <Card.Img variant="top" src={song.imageUrl} alt={song.name} />} {/* Resim URL'sini kullanıyoruz */}
                            <Card.Body>
                                <Card.Title>{song.name}</Card.Title>
                                <Card.Text>{song.artist}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
    );
};

export default TopSongs;
